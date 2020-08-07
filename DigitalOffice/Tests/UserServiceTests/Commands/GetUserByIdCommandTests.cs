using System;
using LT.DigitalOffice.UserService.Commands;
using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace LT.DigitalOffice.UserServiceUnitTests.Commands
{
    public class GetUserByIdCommandTests
    {
        private IGetUserByIdCommand getUserInfoByIdCommand;
        private Mock<IUserRepository> repositoryMock;
        private Mock<IMapper<DbUser, User>> mapperMock;
        private Guid userId;
        private User user;
        private DbUser dbUser;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IUserRepository>();
            mapperMock = new Mock<IMapper<DbUser, User>>();
            getUserInfoByIdCommand = new GetUserByIdCommand(repositoryMock.Object, mapperMock.Object);

            userId = Guid.NewGuid();
            user = new User {Id = userId};
            dbUser = new DbUser{Id = userId};
        }

        [Test]
        public void ShouldReturnsCorrectModelOfUserIfUserExists()
        {
            repositoryMock.Setup(repository => repository.GetUserInfoById(userId)).Returns(dbUser);
            mapperMock.Setup(mapper => mapper.Map(dbUser)).Returns(user);

            var result = getUserInfoByIdCommand.Execute(userId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<User>(result);
            Assert.AreEqual(userId, result.Id);
            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
        }
    }
}