using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Broker.Consumers;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using MassTransit.Testing;
using Moq;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Consumers
{
    public class CheckIfUserHasRightConsumerTests
    {
        private Mock<ICheckRightsRepository> repositoryMock;
        private Mock<IValidator<ICheckIfUserHasRightRequest>> validatorMock;
        private readonly Guid userId = new Guid();
        private const int rightId = 1;
        private InMemoryTestHarness harness;
        private ConsumerTestHarness<CheckIfUserHasRightConsumer> consumerTestHarness;
        
        [SetUp]
        public void SetUp()
        {
            validatorMock = new Mock<IValidator<ICheckIfUserHasRightRequest>>();
            repositoryMock = new Mock<ICheckRightsRepository>();
            harness = new InMemoryTestHarness();
            consumerTestHarness = harness.Consumer(() => new CheckIfUserHasRightConsumer(validatorMock.Object, repositoryMock.Object));
        }
        
        [Test]
        public async Task ShouldConsumeAndPublishWhenRequestIsValidAndRepositoryReturnsSomething()
        {
            repositoryMock
                .Setup(repository => repository.CheckIfUserHasRight(It.IsAny<ICheckIfUserHasRightRequest>()))
                .Returns(true)
                .Verifiable();
            validatorMock.Setup(validator => validator.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(true);
            
            await harness.Start();
            try
            {
                await harness.ConnectRequestClient<ICheckIfUserHasRightRequest>();
                await harness.InputQueueSendEndpoint.Send<ICheckIfUserHasRightRequest>(new
                {
                    UserId = userId,
                    RightId = 1
                });
                
                Assert.That(await harness.Consumed.Any<ICheckIfUserHasRightRequest>());
                Assert.That(await consumerTestHarness.Consumed.Any<ICheckIfUserHasRightRequest>());
                Assert.That(await harness.Published.Any<IOperationResult<bool>>());
                Assert.That(await harness.Published.Any<Fault<IOperationResult<bool>>>(), Is.False);
                repositoryMock.Verify();
                validatorMock.Verify(v => v.Validate(It.IsAny<IValidationContext>()), Times.Once);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task ShouldReturnSuccessfulResponseWhenRequestIsValid()
        {
            repositoryMock
                .Setup(repository => repository.CheckIfUserHasRight(It.IsAny<ICheckIfUserHasRightRequest>()))
                .Returns(true)
                .Verifiable();
            validatorMock.Setup(validator => validator.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(true);

            await harness.Start();
            try
            {
                var requestClient = await harness.ConnectRequestClient<ICheckIfUserHasRightRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserId = userId,
                    RightId = rightId
                });

                Assert.That(response.Message.Body, Is.True);
                Assert.That(response.Message.IsSuccess, Is.True);
                Assert.That(response.Message.Errors, Is.EquivalentTo(new List<string>()));
                Assert.That(consumerTestHarness.Consumed.Select<ICheckIfUserHasRightRequest>().Any(), Is.True);
                Assert.That(harness.Sent.Select<IOperationResult<bool>>().Any(), Is.True);
                repositoryMock.Verify();
                validatorMock.Verify(v => v.Validate(It.IsAny<IValidationContext>()), Times.Once);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task ShouldReturnUnsuccessfulResponseWhenCommandThrowingException()
        {
            const string exceptionMessage = "Exception";
            repositoryMock
                .Setup(repository => repository.CheckIfUserHasRight(It.IsAny<ICheckIfUserHasRightRequest>()))
                .Returns(true);
            validatorMock.Setup(validator => validator.Validate(It.IsAny<IValidationContext>()))
                .Throws(new Exception(exceptionMessage))
                .Verifiable();

            await harness.Start();
            try
            {
                var requestClient = await harness.ConnectRequestClient<ICheckIfUserHasRightRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserId = userId,
                    RightId = rightId
                });

                Assert.That(response.Message.Body, Is.False);
                Assert.That(response.Message.IsSuccess, Is.False);
                Assert.That(response.Message.Errors, Is.EquivalentTo(new List<string>{exceptionMessage}));
                Assert.That(consumerTestHarness.Consumed.Select<ICheckIfUserHasRightRequest>().Any(), Is.True);
                Assert.That(harness.Sent.Select<IOperationResult<bool>>().Any(), Is.True);
                repositoryMock.Verify(
                    repository => repository.CheckIfUserHasRight(It.IsAny<ICheckIfUserHasRightRequest>()),
                    Times.Never);
                validatorMock.Verify();
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}