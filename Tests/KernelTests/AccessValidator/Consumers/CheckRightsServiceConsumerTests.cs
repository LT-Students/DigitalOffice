using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidator.Requests;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.UnitTestLibrary;
using MassTransit;
using MassTransit.Testing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRS = LT.DigitalOffice.CheckRightsService.Broker.Consumers;

namespace KernelUnitTests.AccessValidator.Consumers
{
    public class CheckRightsServiceConsumerTests
    {
        private InMemoryTestHarness harness;
        private IRequestClient<IAccessValidatorRequest> requestClient;

        private Mock<ICheckRightsRepository> repositoryMock;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<ICheckRightsRepository>();
            harness = new InMemoryTestHarness();
            harness.Consumer(() => new CRS.AccessValidatorConsumer(repositoryMock.Object));
        }

        [Test]
        public async Task ShouldRespondIOperationResultBoolTrue()
        {
            await harness.Start();

            repositoryMock
                .Setup(r => r.CheckIfUserHasRight(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(true);

            try
            {
                requestClient = await harness.ConnectRequestClient<IAccessValidatorRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserId = Guid.NewGuid(),
                    RightId = 1
                });

                var expectedResponse = new
                {
                    IsSuccess = true,
                    Errors = null as List,
                    Body = true
                };

                SerializerAssert.AreEqual(expectedResponse, response.Message);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task ShouldRespondIOperationResultBoolFalse()
        {
            await harness.Start();

            repositoryMock
                .Setup(r => r.CheckIfUserHasRight(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns(false);

            try
            {
                requestClient = await harness.ConnectRequestClient<IAccessValidatorRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserId = Guid.NewGuid(),
                    RightId = 1
                });

                var expectedResponse = new
                {
                    IsSuccess = true,
                    Errors = null as List,
                    Body = false
                };

                SerializerAssert.AreEqual(expectedResponse, response.Message);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task ShouldRespondIOperationResultWithErrorsWhenUserRightIsNotFound()
        {
            await harness.Start();

            repositoryMock
                .Setup(r => r.CheckIfUserHasRight(It.IsAny<Guid>(), It.IsAny<int>()))
                .Throws(new Exception("This user or right have not been found."));

            try
            {
                requestClient = await harness.ConnectRequestClient<IAccessValidatorRequest>();

                var response = await requestClient.GetResponse<IOperationResult<bool>>(new
                {
                    UserId = Guid.NewGuid(),
                    RightId = 1
                });

                var expectedResponse = new
                {
                    IsSuccess = false,
                    Errors = new List<string>
                    {
                        "This user or right have not been found."
                    },
                    Body = false
                };

                SerializerAssert.AreEqual(expectedResponse, response.Message);
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
