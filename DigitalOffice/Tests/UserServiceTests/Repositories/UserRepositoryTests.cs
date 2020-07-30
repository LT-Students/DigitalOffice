using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using UserService.Database;
using UserService.Database.Entities;
using UserService.Repositories;
using UserService.Repositories.Interfaces;

namespace UserServiceUnitTests.Repositories
{
    class UserRepositoryTests
    { 
        private IUserRepository repository;
        private DbContextOptions<UserServiceDbContext> dbOptionsCreateUser;
        private UserServiceDbContext dbContext;

        [SetUp]
        public void Initialize()
        {     
            dbOptionsCreateUser = new DbContextOptionsBuilder<UserServiceDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateUserTest")
                .Options;

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
                CertificatesFilesIds = new Collection<DbUserCertificateFile>(),
                AchievementsIds = new Collection<DbUserAchievement>()
            };

            dbContext = new UserServiceDbContext(dbOptionsCreateUser);
            repository = new UserRepository(dbContext);

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
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
                CertificatesFilesIds = new Collection<DbUserCertificateFile>(),
                AchievementsIds = new Collection<DbUserAchievement>()
            };
            Assert.Throws<Exception>(() => repository.UserCreate(user), "Email is already taken.");
        }

        [TearDown]
        public void Clean()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
