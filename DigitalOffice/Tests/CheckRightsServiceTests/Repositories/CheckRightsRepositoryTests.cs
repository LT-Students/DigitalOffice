using System;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Repositories;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.CheckRightsService.RestRequests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Repositories
{
    public class CheckRightsRepositoryTests
    {
        private CheckRightsServiceDbContext dbContext;
        private DbRight dbRight;
        private ICheckRightsRepository repository;

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
            if (dbContext.Database.IsInMemory()) dbContext.Database.EnsureDeleted();
        }

        [Test]
        public void ShouldGetRightsListWhenDbIsNotEmpty()
        {
            dbRight = new DbRight {Id = 0, Name = "Right", Description = "Allows you everything"};
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
        public void ShouldAddRightsForUser()
        {
            dbRight = new DbRight
            {
                Id = 4,
                Name = "Right",
                Description = "Allows you everything",
                UserIds = new List<DbRightUser>()
            };

            var request = new RightsForUserRequest
            {
                UserId = Guid.NewGuid(),
                RightsId = new List<int> {dbRight.Id}
            };

            dbContext.Rights.AddRange(dbRight);
            dbContext.SaveChanges();

            Assert.True(repository.AddRightsToUser(request));
        }

        [Test]
        public void ShouldThrowExceptionWhenRightIdIsNoFound()
        {
            var request = new RightsForUserRequest
            {
                UserId = Guid.NewGuid(),
                RightsId = new List<int> {int.MaxValue, 0}
            };

            Assert.Throws<Exception>(() => repository.AddRightsToUser(request));
        }
    }
}