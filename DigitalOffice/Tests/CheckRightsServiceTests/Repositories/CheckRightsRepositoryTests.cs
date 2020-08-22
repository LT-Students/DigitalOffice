using System;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Repositories;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.CheckRightsServiceUnitTests.UnitTestLibrary;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Repositories
{
    public class CheckRightsRepositoryTests
    {
        private CheckRightsServiceDbContext dbContext;
        private ICheckRightsRepository repository;
        private DbRight dbRight;

        [SetUp]
        public void SetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<CheckRightsServiceDbContext>()
                                    .UseInMemoryDatabase("InMemoryDatabase")
                                    .Options;
            dbContext = new CheckRightsServiceDbContext(dbOptions);
            repository = new CheckRightsRepository(dbContext);
        }

        [TearDown]
        public void Clear()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        [Test]
        public void ShouldGetRightsListWhenDbIsNotEmpty()
        {
            dbRight = new DbRight { Id = 0, Name = "Right", Description = "Allows you everything" };
            dbContext.Rights.Add(dbRight);
            dbContext.SaveChanges();
            
            Assert.That(repository.GetRightsList(), Is.EquivalentTo(new List<DbRight> {dbRight}));
            Assert.That(dbContext.Rights, Is.EquivalentTo(new List<DbRight> {dbRight}));
        }

        [Test]
        public void ShouldGetRightListWhenDbIsEmpty()
        {
            Assert.That(repository.GetRightsList(), Is.Not.Null);
            Assert.That(dbContext.Rights, Is.Empty);
        }

        [Test]
        public void ShouldCheckIfUserHasRightWhenUserHasRight()
        {
            var userId = new Guid();
            const int rightId = 1;
            dbRight = new DbRight {Id = rightId, Name = "Right", UserIds = new List<DbRightUser>()};
            dbRight.UserIds.Add(new DbRightUser{Right = dbRight, RightId = rightId, UserId = userId});
            dbContext.Rights.Add(dbRight);
            dbContext.SaveChanges();
            
            Assert.IsTrue(repository.CheckIfUserHasRight(new CheckIfUserHaveRightRequest(rightId, userId)));
        }

        [Test]
        public void ShouldCheckIfUserHasRightWhenUserDoesNotHaveRight()
        {
            const int rightId = 1;
            dbRight = new DbRight {Id = rightId, Name = "Right", UserIds = new List<DbRightUser>()};
            dbContext.Rights.Add(dbRight);
            dbContext.SaveChanges();

            Assert.IsFalse(repository.CheckIfUserHasRight(new CheckIfUserHaveRightRequest(rightId, new Guid())));
        }

        [Test]
        public void ShouldCheckIfUserHasRightWhenDatabaseDoesNotContainSuchRight()
        {
            Assert.IsFalse(repository.CheckIfUserHasRight(new CheckIfUserHaveRightRequest(1, new Guid())));
        }
    }
}