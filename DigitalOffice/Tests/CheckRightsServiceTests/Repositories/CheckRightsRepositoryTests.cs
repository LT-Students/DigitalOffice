using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Mappers.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.CheckRightsService.RestRequests;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Repositories
{
    class CheckRightsRepositoryTests
    {
        private CheckRightsServiceDbContext dbContext;
        private ICheckRightsRepository repository;
        private Mock<IMapper<DbRight, Right>> mapperMock;
        private DbRight dbRight;
        private DbRight dbRightUpdate;

        [SetUp]
        public void SetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<CheckRightsServiceDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            dbContext = new CheckRightsServiceDbContext(dbOptions);
            mapperMock = new Mock<IMapper<DbRight, Right>>();
            repository = new CheckRightsRepository(dbContext, mapperMock.Object);

            dbRight = new DbRight
            {
                Id = 3,
                Name = "Right",
                Description = "Allows you everything",
                UserIds = new List<DbRightUser>()
            };
            dbRightUpdate = new DbRight
            {
                Id = 4,
                Name = "Right update",
                Description = "Allows you update everything",
                UserIds = new List<DbRightUser>()
            };

            dbContext.Rights.AddRange(dbRight, dbRightUpdate);
            dbContext.SaveChanges();

            mapperMock.Setup(mapper => mapper.Map(dbRight)).Returns(new Right
                {Id = dbRight.Id, Name = dbRight.Name, Description = dbRight.Description});
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
        public void GetRightsListSuccessfully()
        {
            var resultRightsList = repository.GetRightsList();

            Assert.DoesNotThrow(() => repository.GetRightsList());
            Assert.IsNotNull(resultRightsList);
        }

        [Test]
        public void AddRightsForUserSuccessfully()
        {
            var request = new RightsForUserRequest
            {
                UserId = Guid.NewGuid(),
                RightsId = new List<int> {dbRight.Id, dbRightUpdate.Id}
            };

            var dbRightUser = new DbRightUser
            {
                UserId = request.UserId,
                Right = dbRight,
                RightId = dbRight.Id
            };

            var dbRightUserUpdate = new DbRightUser
            {
                UserId = request.UserId,
                Right = dbRightUpdate,
                RightId = dbRightUpdate.Id
            };

            Assert.True(repository.AddRightsToUser(request));

            Assert.AreEqual(dbRightUser.RightId, dbContext.Rights.Find(dbRight.Id).UserIds.First().RightId);
            Assert.AreEqual(dbRightUser.UserId, dbContext.Rights.Find(dbRight.Id).UserIds.First().UserId);

            Assert.AreEqual(dbRightUserUpdate.RightId, dbContext.Rights.Find(dbRightUpdate.Id).UserIds.First().RightId);
            Assert.AreEqual(dbRightUserUpdate.UserId, dbContext.Rights.Find(dbRightUpdate.Id).UserIds.First().UserId);
        }

        [Test]
        public void ShouldThrowExceptionAddRightsForUser()
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