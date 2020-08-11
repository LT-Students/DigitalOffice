using FluentValidation;
using LT.DigitalOffice.UserService.Commands;
using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using LT.DigitalOffice.UserService.Validators;
using Moq;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.UserServiceUnitTests.Commands
{
    public class UserCreateCommandTests
    {
        private Mock<IUserRepository> repositoryMock;
        private IUserCreateCommand command;
        private IValidator<UserCreateRequest> validator;
        private IMapper<UserCreateRequest, DbUser> mapper;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IUserRepository>();
            mapper = new UserMapper();
            validator = new UserCreateRequestValidator();

            command = new UserCreateCommand(validator, repositoryMock.Object, mapper);
        }

        [Test]
        public void ShouldThrowExceptionWhenUserDataIsInvalid()
        {
            var request = new UserCreateRequest
            {
                FirstName = "Example1",
                LastName = "Example",
                MiddleName = "Example",
                Email = "Example@gmail.com",
                Status = "Example",
                Password = "Example",
                IsAdmin = false
            };

            Assert.Throws<ValidationException>(() => command.Execute(request));
        }

        [Test]
        public void ShouldThrowExceptionWhenEmailIsAlreadyTaken()
        {
            repositoryMock
                .Setup(x => x.UserCreate(It.IsAny<DbUser>()))
                .Throws(new Exception("Email is already taken."));

            var request = new UserCreateRequest
            {
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Email = "Example@gmail.com",
                Status = "Example",
                Password = "Example",
                IsAdmin = false
            };

            Assert.Throws<Exception>(() => command.Execute(request), "Email is already taken.");
        }

        [Test]
        public void ShouldCreateUserWhenUserDataIsValid()
        {

            repositoryMock
                .Setup(x => x.UserCreate(It.IsAny<DbUser>()))
                .Returns(true);

            var request = new UserCreateRequest
            {
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Email = "Example@gmail.com",
                Status = "Example",
                Password = "Example",
                IsAdmin = false
            };

            Assert.IsTrue(command.Execute(request));
        }
    }
}