using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;
using UserService.Commands;
using UserService.Commands.Interfaces;
using UserService.Database.Entities;
using UserService.Mappers;
using UserService.Mappers.Interfaces;
using UserService.Repositories.Interfaces;
using UserService.RestRequests;
using UserService.Validators;

namespace UserServiceUnitTests.Commands
{
    class UserCreateCommandTests
    {
        private Mock<IUserRepository> repositoryMock;
        private IUserCreateCommand command;
        private IValidator<UserCreateRequest> validator;
        private IMapper<UserCreateRequest, DbUser> mapper;

        [SetUp]
        public void Initialization()
        {
            repositoryMock = new Mock<IUserRepository>();
            mapper = new UserCreateRequestToDbUserMapper();
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
                Password = "Example"
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
                Password = "Example"
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
                Password = "Example"
            };

            Assert.IsTrue(command.Execute(request));
        }
    }
}
