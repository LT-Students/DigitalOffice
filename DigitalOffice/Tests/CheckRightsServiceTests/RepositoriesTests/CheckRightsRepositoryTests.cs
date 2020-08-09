using System;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Mappers.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.CheckRightsServiceUnitTests.UnitTestLibrary;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.RepositoriesTests
{
    class CheckRightsRepositoryTests
    {
        private CheckRightsServiceDbContext dbContext;
        private ICheckRightsRepository repository;
        private Mock<IMapper<DbRight, Right>> mapperMock;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mapperMock = new Mock<IMapper<DbRight, Right>>();
        }
        
        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<CheckRightsServiceDbContext>()
                                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                                    .Options;
            dbContext = new CheckRightsServiceDbContext(dbOptions);
            
            repository = new CheckRightsRepository(dbContext, mapperMock.Object);
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
            var dbRight = new DbRight { Id = 0, Name = "Right", Description = "Allows you everything" };
            dbContext.Rights.Add(dbRight);
            dbContext.SaveChanges();
            mapperMock.Setup(mapper => mapper.Map(dbRight)).Returns(new Right { Id = dbRight.Id, Name = dbRight.Name, Description = dbRight.Description });
            
            var resultRightsList = repository.GetRightsList();

            Assert.DoesNotThrow(() => repository.GetRightsList());
            Assert.IsNotNull(resultRightsList);
        }

        [Test]
        public void CheckIfUserHaveRightReturnsTrueIfUserHaveRight()
        {
            var userId = new Guid();
            const int rightId = 1;
            var dbRight = new DbRight {Id = rightId, Name = "Right", UserIds = new List<DbRightUser>()};
            dbRight.UserIds.Add(new DbRightUser{Right = dbRight, RightId = rightId, UserId = userId});
            dbContext.Rights.Add(dbRight);
            dbContext.SaveChanges();
            
            Assert.IsTrue(repository.CheckIfUserHaveRight(new CheckIfUserHaveRightRequest(rightId, userId)));
        }

        [Test]
        public void CheckIfUserHaveRightReturnsFalseIfUserHaveNotRight()
        {
            const int rightId = 1;
            var dbRight = new DbRight {Id = rightId, Name = "Right", UserIds = new List<DbRightUser>()};
            dbContext.Rights.Add(dbRight);
            dbContext.SaveChanges();
            
            Assert.IsFalse(repository.CheckIfUserHaveRight(new CheckIfUserHaveRightRequest(rightId, new Guid())));
        }

        [Test]
        public void CheckIfUserHaveRightReturnsFalseIfDatabaseDoesNotContainSuchRight()
        {
            Assert.IsFalse(repository.CheckIfUserHaveRight(new CheckIfUserHaveRightRequest(1, new Guid())));
        }
    }
}