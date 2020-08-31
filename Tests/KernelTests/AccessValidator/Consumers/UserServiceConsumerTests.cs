using LT.DigitalOffice.Kernel.AccessValidator.Requests;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.UnitTestLibrary;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using MassTransit;
using MassTransit.Testing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using US = LT.DigitalOffice.UserService.Broker.Consumers;

namespace KernelUnitTests.AccessValidator.Consumers
{
    public class UserServiceConsumerTests
    {
        private DbUser adminUser;
        private DbUser notAdminUser;
        private InMemoryTestHarness harness;
        private IRequestClient<IAccessValidatorRequest> requestClient;

        private Mock<IUserRepository> repositoryMock;

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

            repositoryMock = new Mock<IUserRepository>();

            harness = new InMemoryTestHarness();
            harness.Consumer(() => new US.AccessValidatorConsumer(repositoryMock.Object));

            await harness.Start();
        }

        [Test]
        public async Task ShouldRespondIOperationResultBoolTrue()
        {
            repositoryMock
                .Setup(r => r.GetUserInfoById(It.IsAny<Guid>()))
                .Returns(adminUser);

            requestClient = await harness.ConnectRequestClient<IAccessValidatorRequest>();

            var response = await requestClient.GetResponse<IOperationResult<bool>>(new
            {
                UserId = Guid.NewGuid()
            });

            var expectedResponse = new
            {
                IsSuccess = true,
                Errors = null as List,
                Body = true
            };

            SerializerAssert.AreEqual(expectedResponse, response.Message);
        }

        [Test]
        public async Task ShouldRespondIOperationResultBoolFalse()
        {
            repositoryMock
                .Setup(r => r.GetUserInfoById(It.IsAny<Guid>()))
                .Returns(notAdminUser);

            requestClient = await harness.ConnectRequestClient<IAccessValidatorRequest>();

            var response = await requestClient.GetResponse<IOperationResult<bool>>(new
            {
                UserId = Guid.NewGuid()
            });

            var expectedResponse = new
            {
                IsSuccess = true,
                Errors = null as List,
                Body = false
            };

            SerializerAssert.AreEqual(expectedResponse, response.Message);
        }

        [Test]
        public async Task ShouldRespondIOperationResultWithErrorsWhenUserIsNotFound()
        {
            repositoryMock
                .Setup(r => r.GetUserInfoById(It.IsAny<Guid>()))
                .Throws(new Exception("User with this id is not found."));

            requestClient = await harness.ConnectRequestClient<IAccessValidatorRequest>();

            var response = await requestClient.GetResponse<IOperationResult<bool>>(new
            {
                UserId = Guid.NewGuid()
            });

            var expectedResponse = new
            {
                IsSuccess = false,
                Errors = new List<string>
                {
                    "User with this id is not found."
                },
                Body = false
            };

            SerializerAssert.AreEqual(expectedResponse, response.Message);
        }

        [OneTimeTearDown]
        public async Task StopHarness()
        {
            await harness.Stop();
        }
    }
}
