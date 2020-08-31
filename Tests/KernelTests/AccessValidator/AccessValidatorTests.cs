using LT.DigitalOffice.Kernel.AccessValidator.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using US = LT.DigitalOffice.UserService.Broker.Consumers;
using CRS = LT.DigitalOffice.CheckRightsService.Broker.Consumers;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using AV = LT.DigitalOffice.Kernel.AccessValidator;
using LT.DigitalOffice.UserService.Database.Entities;
using System.Net;
using Microsoft.Extensions.Primitives;

namespace KernelUnitTests.AccessValidator
{
    public class AccessValidatorTests
    {
        private DbUser adminUser;
        private DbUser notAdminUser;
        private string userId;
        private IAccessValidator accessValidator;

        private const string USER_SERVICE_TEST_QUEUE = "UserService";
        private const string USER_SERVICE_TEST_REQUEST_URL = "loopback://localhost/UserService";
        private const string CHECK_RIGHTS_SERVICE_TEST_QUEUE = "CheckRightsService";
        private const string CHECK_RIGHTS_SERVICE_TEST_REQUEST_URL = "loopback://localhost/CheckRightsService";
        private const int RIGHT_ID = 5;

        private InMemoryTestHarness harness;

        private Mock<IHttpContextAccessor> httpContextMock;
        private Mock<IOptions<RabbitMQOptions>> optionsMock;
        private Mock<IUserRepository> userServiceRepositoryMock;
        private Mock<ICheckRightsRepository> checkRightsServiceRepositoryMock;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            adminUser = new DbUser
            {
                IsAdmin = true
            };
            notAdminUser = new DbUser
            {
                IsAdmin = false
            };
            userId = Guid.NewGuid().ToString();

            userServiceRepositoryMock = new Mock<IUserRepository>();
            checkRightsServiceRepositoryMock = new Mock<ICheckRightsRepository>();

            harness = new InMemoryTestHarness();

            harness.Consumer(
                () => new US.AccessValidatorConsumer(
                    userServiceRepositoryMock.Object),
                    USER_SERVICE_TEST_QUEUE);
            harness.Consumer(
                () => new CRS.AccessValidatorConsumer(
                    checkRightsServiceRepositoryMock.Object),
                    CHECK_RIGHTS_SERVICE_TEST_QUEUE);

            httpContextMock = new Mock<IHttpContextAccessor>();

            optionsMock = new Mock<IOptions<RabbitMQOptions>>();
            optionsMock
                .Setup(r => r.Value)
                .Returns(new RabbitMQOptions
                {
                    AccessValidatorUserServiceURL = USER_SERVICE_TEST_REQUEST_URL,
                    AccessValidatorCheckRightsServiceURL = CHECK_RIGHTS_SERVICE_TEST_REQUEST_URL
                });

            // Allows to pass harness.Bus property into the AccessValidator constructor. Otherwise the bus is null.
            await harness.Start();

            accessValidator = new AV.AccessValidator(harness.Bus, optionsMock.Object, httpContextMock.Object);
        }

        [Test]
        public async Task ShouldReturnTrueWhenUserIsAdmin()
        {
            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns(userId);

            userServiceRepositoryMock
                .Setup(r => r.GetUserInfoById(It.IsAny<Guid>()))
                .Returns(adminUser);

            var result = await accessValidator.IsAdmin();
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task ShouldReturnFalseWhenUserIsNotAdmin()
        {
            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns(userId);

            userServiceRepositoryMock
                .Setup(r => r.GetUserInfoById(It.IsAny<Guid>()))
                .Returns(notAdminUser);

            var result = await accessValidator.IsAdmin();
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task ShouldReturnTrueWhenUserHasRights()
        {
            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns(userId);

            checkRightsServiceRepositoryMock
                .Setup(r => r.CheckIfUserHasRight(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(true);

            var result = await accessValidator.HasRights(RIGHT_ID);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task ShouldReturnFalseWhenUserDoesntHaveRights()
        {
            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns(userId);

            checkRightsServiceRepositoryMock
                .Setup(r => r.CheckIfUserHasRight(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(false);

            var result = await accessValidator.HasRights(RIGHT_ID);
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task ShouldReturnFalseWhenCheckRightsServiceConsumerRespondsWithErrors()
        {
            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns(userId);

            checkRightsServiceRepositoryMock
                .Setup(r => r.CheckIfUserHasRight(It.IsAny<Guid>(), It.IsAny<int>()))
                .Throws(new Exception("Such user does not have such right."));

            var result = await accessValidator.HasRights(RIGHT_ID);
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task ShouldReturnFalseWhenUserServiceConsumerRespondsWithErrors()
        {
            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns(userId);

            userServiceRepositoryMock
                .Setup(r => r.GetUserInfoById(It.IsAny<Guid>()))
                .Throws(new Exception("User is not found."));

            var result = await accessValidator.IsAdmin();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ShouldThrowFormatExceptionWhenThereIsInvalidGuidInHeaders()
        {
            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns("SampleText");

                Assert.ThrowsAsync<FormatException>(async () => await accessValidator.IsAdmin());
                Assert.ThrowsAsync<FormatException>(async () => await accessValidator.HasRights(RIGHT_ID));
        }

        [Test]
        public void ShouldThrowNullReferenceExceptionWhenThereIsNoUserIdInHeaders()
        {
            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns<StringValues>(null);

            Assert.ThrowsAsync<NullReferenceException>(async () => await accessValidator.IsAdmin());
            Assert.ThrowsAsync<NullReferenceException>(async () => await accessValidator.HasRights(RIGHT_ID));
        }

        [Test]
        public void ShouldThrowListenerExceptionWhenThereIsMoreThanOneUserIdInHeaders()
        {
            var stringValues = new StringValues(new string[]
            {
                "Guid1", "Guid2"
            });

            httpContextMock
                .Setup(h => h.HttpContext.Request.Headers["UserId"])
                .Returns(stringValues);

            Assert.ThrowsAsync<HttpListenerException>(async () => await accessValidator.IsAdmin());
            Assert.ThrowsAsync<HttpListenerException>(async () => await accessValidator.HasRights(RIGHT_ID));
        }

        [OneTimeTearDown]
        public async Task StopHarness()
        {
            await harness.Stop();
        }
    }
}