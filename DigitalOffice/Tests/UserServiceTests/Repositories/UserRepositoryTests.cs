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
        private DbUser firstUser = new DbUser
        {
            Id = Guid.NewGuid(),
            Email = "Example@gmail.com",
            FirstName = "Example",
            LastName = "Example",
            MiddleName = "Example",
            Status = "Example",
            PasswordHash =
                Encoding.Default.GetString(new SHA512Managed().ComputeHash(Encoding.Default.GetBytes("Example"))),
            AvatarFileId = null,
            IsActive = true,
            IsAdmin = false,
            CertificatesFilesIds = new Collection<DbUserCertificateFile>(),
            AchievementsIds = new Collection<DbUserAchievement>()
        };

        private DbUser secondUser = new DbUser
        {
            Id = Guid.NewGuid(),
            Email = "DifferentEmail@gmail.com",
            FirstName = "Example",
            LastName = "Example",
            MiddleName = "Example",
            Status = "Example",
            PasswordHash = Encoding.Default.GetString(new SHA512Managed().ComputeHash(Encoding.Default.GetBytes("Example"))),
            AvatarFileId = null,
            IsActive = true,
            IsAdmin = false,
            CertificatesFilesIds = new Collection<DbUserCertificateFile>(),
            AchievementsIds = new Collection<DbUserAchievement>()
        };

        private const string UserNotFoundExceptionMessage = "User with this id not found.";
        private const string EmailAlreadyTakenExceptionMessage = "Email is already taken.";

        private UserServiceDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<UserServiceDbContext>()
                .UseInMemoryDatabase("InMemoryDatabase")
                .Options;

            return new UserServiceDbContext(options);
        }

        [SetUp]
        public void SetUp()
        {
            dbContext = GetMemoryContext();
            repository = new UserRepository(dbContext);

            dbContext.Users.Add(firstUser);
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
        public void ShouldThrowExceptionWhenUserWithRequiredIdDoesNotExist()
        {
            Assert.That(() => repository.GetUserInfoById(Guid.Empty),
                Throws.TypeOf<Exception>().And.Message.EqualTo(UserNotFoundExceptionMessage));
        }

        [Test]
        public void ShouldReturnUserWhenUserWithRequiredIdExists()
        {
            var resultUser = repository.GetUserInfoById(firstUser.Id);

            Assert.IsInstanceOf<DbUser>(resultUser);
            SerializerAssert.AreEqual(firstUser, resultUser);
            Assert.That(dbContext.Users, Is.EquivalentTo(new[] {firstUser}));
        }

        [Test]
        public void ShouldCreateUserWhenUserDataIsValid()
        {
            Assert.That(repository.UserCreate(secondUser),Is.EqualTo(secondUser.Id));
            Assert.That(dbContext.Users, Is.EquivalentTo(new[] {firstUser, secondUser}));
        }

        [Test]
        public void ShouldThrowExceptionWhenEmailIsAlreadyTaken()
        {
            Assert.That(() => repository.UserCreate(firstUser),
                Throws.Exception.TypeOf<Exception>().And.Message.EqualTo(EmailAlreadyTakenExceptionMessage));
            Assert.That(dbContext.Users, Is.EquivalentTo(new[] {firstUser}));
        }
    }
}