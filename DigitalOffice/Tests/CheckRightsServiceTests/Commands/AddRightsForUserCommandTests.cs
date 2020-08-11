using System;
using System.Collections.Generic;
using FluentValidation;
using LT.DigitalOffice.CheckRightsService.Commands;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.CheckRightsService.RestRequests;
using Moq;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Commands
{
    public class AddRightsForUserCommandTests
    {
        private Mock<ICheckRightsRepository> repositoryMock;
        private Mock<IValidator<RightsForUserRequest>> validatorMock;
        private IAddRightsForUserCommand command;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<ICheckRightsRepository>();
            validatorMock = new Mock<IValidator<RightsForUserRequest>>();
            command = new AddRightsForUserCommand(repositoryMock.Object, validatorMock.Object);
        }

        [Test]
        public void ShouldAddRightsForUser()
        {
            var request = new RightsForUserRequest
            {
                UserId = Guid.NewGuid(),
                RightsId = new List<int>() {0, 1}
            };

            validatorMock
                .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(true);

            repositoryMock
                .Setup(x => x.AddRightsToUser(It.IsAny<RightsForUserRequest>()))
                .Returns(true);

            Assert.True(command.Execute(request));
        }

        [Test]
        public void ShouldThrowValidationExceptionIfUserIdIsEmpty()
        {
            var request = new RightsForUserRequest
            {
                RightsId = new List<int>() {0, 1}
            };

            validatorMock
                .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(false);

            Assert.Throws<ValidationException>(() => command.Execute(request));
        }
    }
}