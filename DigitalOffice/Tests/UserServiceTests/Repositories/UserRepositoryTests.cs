using LT.DigitalOffice.UserService.Database;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Repositories;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using LT.DigitalOffice.UserServiceUnitTests.UnitTestLibrary;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace LT.DigitalOffice.UserServiceUnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private UserServiceDbContext dbContext;
        private IUserRepository repository;
        private DbUser dbUser;

        private const string ExceptionMessage = "User with this id not found.";

        private UserServiceDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<UserServiceDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            return new UserServiceDbContext(options);
        }

        [SetUp]
        public void SetUp()
        {
            dbContext = GetMemoryContext();
            repository = new UserRepository(dbContext);

            dbUser = new DbUser
            {
                Id = Guid.NewGuid(),
                Email = "Example@gmail.com",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Status = "Example",
                PasswordHash = (new SHA512Managed().ComputeHash(Encoding.Default.GetBytes("Example"))).ToString(),
                AvatarFileId = null,
                IsActive = true,
                IsAdmin = false,
                CertificatesFilesIds = new Collection<DbUserCertificateFile>(),
                AchievementsIds = new Collection<DbUserAchievement>()
            };

            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        [Test]
        public void ShouldThrowExceptionIfUserWithRequiredIdDoesNotExist()
        {
            Assert.Throws<Exception>(() => repository.GetUserInfoById(Guid.Empty), ExceptionMessage);
        }

        [Test]
        public void ShouldReturnUserIfUserWithRequiredIdExists()
        {
            var resultUser = repository.GetUserInfoById(dbUser.Id);

            Assert.IsNotNull(resultUser);
            Assert.IsInstanceOf<DbUser>(resultUser);
            SerializerAssert.AreEqual(dbUser, resultUser);
        }

        [Test]
        public void ShouldCreateUserWhenUserDataIsCorrect()
        {
            var user = new DbUser
            {
                Id = Guid.NewGuid(),
                Email = "Example1@gmail.com",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Status = "Example",
                PasswordHash = (new SHA512Managed().ComputeHash(Encoding.Default.GetBytes("Example"))).ToString(),
                AvatarFileId = null,
                IsActive = true,
                IsAdmin = false,
                CertificatesFilesIds = new Collection<DbUserCertificateFile>(),
                AchievementsIds = new Collection<DbUserAchievement>()
            };

            Assert.True(repository.UserCreate(user));
        }

        [Test]
        public void ShouldThrowExceptionWhenEmailIsAlreadyTaken()
        {
            var user = new DbUser
            {
                Id = Guid.NewGuid(),
                Email = "Example@gmail.com",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Status = "Example",
                PasswordHash = (new SHA512Managed().ComputeHash(Encoding.Default.GetBytes("Example"))).ToString(),
                AvatarFileId = null,
                IsActive = true,
                IsAdmin = false,
                CertificatesFilesIds = new Collection<DbUserCertificateFile>(),
                AchievementsIds = new Collection<DbUserAchievement>()
            };

            Assert.Throws<Exception>(() => repository.UserCreate(user), "Email is already taken.");
        }
    }
}