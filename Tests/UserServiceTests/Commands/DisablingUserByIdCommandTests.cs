﻿using LT.DigitalOffice.Kernel.AccessValidator.Interfaces;
using LT.DigitalOffice.UserService.Commands;
using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.UserServiceUnitTests.Commands
{
    public class DisablingUserByIdCommandTests
    {
        #region Variables declaration
        private Mock<IUserRepository> repositoryMock;
        private Mock<IAccessValidator> accessValidatorMock;

        private IDisableUserByIdCommand command;

        private Guid userId;
        private DbUser newDbUser;
        private Guid requestingUserId;
        #endregion

        #region Setup
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            accessValidatorMock = new Mock<IAccessValidator>();
            userId = Guid.NewGuid();
            requestingUserId = Guid.NewGuid();

            newDbUser = new DbUser
            {
                Id = userId,
                Email = "Example",
                FirstName = "Example1",
                LastName = "Example1",
                MiddleName = "Example1",
                Status = "normal",
                PasswordHash = "Example1",
                AvatarFileId = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false
            };
        }

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IUserRepository>();
            command = new DisableUserByIdCommand(repositoryMock.Object, accessValidatorMock.Object);
        }
        #endregion

        [Test]
        public void ShouldThrowExceptionWhenUserIdNotFoundInDb()
        {
            accessValidatorMock
                .Setup(x => x.IsAdmin())
                .Returns(Task.FromResult(true));

            accessValidatorMock
                .Setup(x => x.HasRights(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            repositoryMock
                .Setup(x => x.GetUserInfoById(userId))
                .Throws(new Exception());

            Assert.ThrowsAsync<Exception>(() => command.ExecuteAsync(userId, requestingUserId));
            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Never);
        }

        [Test]
        public void ShouldThrowExceptionWhenEditingUserInDb()
        {
            accessValidatorMock
                .Setup(x => x.IsAdmin())
                .Returns(Task.FromResult(true));

            accessValidatorMock
                .Setup(x => x.HasRights(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            repositoryMock
                .Setup(x => x.GetUserInfoById(userId))
                .Returns(newDbUser);

            repositoryMock
                .Setup(x => x.EditUser(It.IsAny<DbUser>()))
                .Throws(new Exception());

            Assert.ThrowsAsync<Exception>(() => command.ExecuteAsync(userId, requestingUserId));
            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Once);
        }

        [Test]
        public void ShouldUserDisabledSuccess()
        {
            accessValidatorMock
                .Setup(x => x.IsAdmin())
                .Returns(Task.FromResult(true));

            accessValidatorMock
                .Setup(x => x.HasRights(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            repositoryMock
                .Setup(x => x.GetUserInfoById(userId))
                .Returns(newDbUser);

            repositoryMock
                .Setup(x => x.EditUser(It.IsAny<DbUser>()))
                .Returns(true);

            command.ExecuteAsync(userId, requestingUserId);

            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Once);
        }

        [Test]
        public void ShouldUserDisabledSuccessWhenUserIsAdmin()
        {
            accessValidatorMock
                .Setup(x => x.IsAdmin())
                .Returns(Task.FromResult(true));

            repositoryMock
                .Setup(x => x.GetUserInfoById(userId))
                .Returns(newDbUser);

            repositoryMock
                .Setup(x => x.EditUser(It.IsAny<DbUser>()))
                .Returns(true);

            command.ExecuteAsync(userId, requestingUserId);

            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Once);
        }

        [Test]
        public void ShouldUserDisabledSuccessWhenUserIsNotAdminAndHasRights()
        {
            accessValidatorMock
                .Setup(x => x.IsAdmin())
                .Returns(Task.FromResult(false));

            accessValidatorMock
                .Setup(x => x.HasRights(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            repositoryMock
                .Setup(x => x.GetUserInfoById(userId))
                .Returns(newDbUser);

            repositoryMock
                .Setup(x => x.EditUser(It.IsAny<DbUser>()))
                .Returns(true);

            command.ExecuteAsync(userId, requestingUserId);

            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Once);
        }

        [Test]
        public void ShouldThrowExceptionWhenUserHasNotEnoughRights()
        {
            accessValidatorMock
                .Setup(x => x.IsAdmin())
                .Returns(Task.FromResult(false));

            accessValidatorMock
                .Setup(x => x.HasRights(It.IsAny<int>()))
                .Returns(Task.FromResult(false));

            Assert.That(() => command.ExecuteAsync(userId, requestingUserId),
                Throws.InstanceOf<Exception>().And.Message.EqualTo("Not enough rights."));
            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Never);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Never);
        }
    }
}
