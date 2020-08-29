using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Controllers;
using LT.DigitalOffice.UserService.Models;
using Moq;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.UserServiceUnitTests.Controllers
{
    class UserControllerTests
    {
        private UserController userController;
        private Mock<IGetUserByEmailCommand> commandMock;
        private User user = new User();

        [SetUp]
        public void SetUp()
        {
            userController = new UserController();
            commandMock = new Mock<IGetUserByEmailCommand>();
        }

        [Test]
        public void ShouldThrowExceptionWhenCommandThrowsIt()
        {
            commandMock.Setup(command => command.Execute(It.IsAny<string>()))
                .Throws<Exception>();

            Assert.Throws<Exception>(() =>
                userController.GetUserByEmail(commandMock.Object, It.IsAny<string>()));
        }

        [Test]
        public void ShouldReturnTheSameValueAsCommandWhenCreateNewProject()
        {
            commandMock.Setup(command => command.Execute(It.IsAny<string>()))
                .Returns(user);

            Assert.That(
                userController.GetUserByEmail(commandMock.Object, It.IsAny<string>()),
                Is.EqualTo(user));
        }
    }
}
