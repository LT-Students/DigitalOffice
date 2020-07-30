using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Mappers.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
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

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<CheckRightsServiceDbContext>()
                                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                                    .Options;
            dbContext = new CheckRightsServiceDbContext(dbOptions);
            mapperMock = new Mock<IMapper<DbRight, Right>>();
            repository = new CheckRightsRepository(dbContext, mapperMock.Object);

            var dbRight = new DbRight { Id = 0, Name = "Right", Description = "Allows you everything" };
            dbContext.Rights.Add(dbRight);
            dbContext.SaveChanges();
            mapperMock.Setup(mapper => mapper.Map(dbRight)).Returns(new Right { Id = dbRight.Id, Name = dbRight.Name, Description = dbRight.Description });
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
    }
}
