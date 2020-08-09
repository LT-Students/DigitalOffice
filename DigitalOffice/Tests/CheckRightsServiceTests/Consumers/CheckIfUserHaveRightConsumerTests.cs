using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Broker.Consumers;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using MassTransit.Testing;
using Moq;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Consumers
{
    public class CheckIfUserHaveRightConsumerTests
    {
        private readonly Mock<ICheckIfUserHaveRightCommand> commandMock = new Mock<ICheckIfUserHaveRightCommand>();
        private readonly Guid userId = new Guid();
        private const int rightId = 1;
        private InMemoryTestHarness harness;
        private ConsumerTestHarness<CheckIfUserHaveRightConsumer> consumerTestHarness;
        
        [SetUp]
        public void SetUp()
        {
            harness = new InMemoryTestHarness();
            consumerTestHarness = harness.Consumer(() => new CheckIfUserHaveRightConsumer(commandMock.Object));
        }
        
        [Test]
        public async Task ShouldConsumeAndPublishIfCommandReturnsSomething()
        {
            commandMock.Setup(command => command.Execute(It.IsAny<ICheckIfUserHaveRightRequest>()))
                .Returns(true);
            
            await harness.Start();
            try
            {
                await harness.ConnectRequestClient<ICheckIfUserHaveRightRequest>();
                await harness.InputQueueSendEndpoint.Send<ICheckIfUserHaveRightRequest>(new
                {
                    UserId = userId,
                    RightId = 1
                });
                
                Assert.That(await harness.Consumed.Any<ICheckIfUserHaveRightRequest>());
                Assert.That(await consumerTestHarness.Consumed.Any<ICheckIfUserHaveRightRequest>());
                Assert.That(await harness.Published.Any<IOperationResult<bool>>());
                Assert.That(await harness.Published.Any<Fault<IOperationResult<bool>>>(), Is.False);
                commandMock.Verify();
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task ShouldReturnSuccessfulResponseIfCommandDoesNotThrowException()
        {
            commandMock.Setup(command => command.Execute(It.IsAny<ICheckIfUserHaveRightRequest>()))
                .Returns(true);

            await harness.Start();
            try
            {
                var requestClient = await harness.ConnectRequestClient<ICheckIfUserHaveRightRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserId = userId,
                    RightId = rightId
                });

                Assert.That(response.Message.Body, Is.True);
                Assert.That(response.Message.IsSuccess, Is.True);
                Assert.That(response.Message.Errors, Is.EquivalentTo(new List<string>()));
                Assert.That(consumerTestHarness.Consumed.Select<ICheckIfUserHaveRightRequest>().Any(), Is.True);
                Assert.That(harness.Sent.Select<IOperationResult<bool>>().Any(), Is.True);
                commandMock.Verify();
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task ShouldReturnUnsuccessfulResponseIfCommandThrowException()
        {
            const string exceptionMessage = "Exception";
            commandMock.Setup(command => command.Execute(It.IsAny<ICheckIfUserHaveRightRequest>()))
                .Throws(new Exception(exceptionMessage));
            
            await harness.Start();
            try
            {
                var requestClient = await harness.ConnectRequestClient<ICheckIfUserHaveRightRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserId = userId,
                    RightId = rightId
                });

                Assert.That(response.Message.Body, Is.False);
                Assert.That(response.Message.IsSuccess, Is.False);
                Assert.That(response.Message.Errors, Is.EquivalentTo(new List<string>{exceptionMessage}));
                Assert.That(consumerTestHarness.Consumed.Select<ICheckIfUserHaveRightRequest>().Any(), Is.True);
                Assert.That(harness.Sent.Select<IOperationResult<bool>>().Any(), Is.True);
                commandMock.Verify();
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}