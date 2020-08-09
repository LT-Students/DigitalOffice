using System;
using FluentValidation;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Commands;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.CheckRightsServiceUnitTests.UnitTestLibrary;
using Moq;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.CommandsTests
{
    public class CheckIfUserHaveRightCommandTests
    {
        private Mock<IValidator<ICheckIfUserHaveRightRequest>> validatorMock;
        private Mock<ICheckRightsRepository> repositoryMock;
        private ICheckIfUserHaveRightCommand command;

        [SetUp]
        public void OneTimeSetUp()
        {
            validatorMock = new Mock<IValidator<ICheckIfUserHaveRightRequest>>();
            repositoryMock = new Mock<ICheckRightsRepository>();
            command = new CheckIfUserHaveRightCommand(repositoryMock.Object, validatorMock.Object);
        }

        [Test]
        public void ExecuteShouldReturnValueFromRepositoryIfRequestIsValid()
        {
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(true);

            repositoryMock
                .Setup(repository => repository.CheckIfUserHaveRight(It.IsAny<ICheckIfUserHaveRightRequest>()))
                .Returns(true)
                .Verifiable();
            
            Assert.That(command.Execute(new CheckIfUserHaveRightRequest(1, new Guid())), Is.True);
            validatorMock.Verify(v => v.Validate(It.IsAny<IValidationContext>()), Times.Once);
            repositoryMock.Verify();
        }

        [Test]
        public void ExecuteShouldThrowValidationExceptionIfRequestIsInvalid()
        {
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<IValidationContext>()))
                .Throws<Exception>()
                .Verifiable();
            
            Assert.Throws<Exception>(() => command.Execute(new CheckIfUserHaveRightRequest(1, new Guid())));
            validatorMock.Verify();
            repositoryMock.Verify(repository => repository.CheckIfUserHaveRight(It.IsAny<ICheckIfUserHaveRightRequest>()), Times.Never);
        }
    }
}