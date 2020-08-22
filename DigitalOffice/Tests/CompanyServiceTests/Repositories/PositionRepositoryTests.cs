using System;
using System.Collections.Generic;
using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Repositories;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LT.DigitalOffice.CompanyServiceUnitTests.Repositories
{
    internal class PositionRepositoryTests
    {
        private const string PositionName = "Software Engineer";
        private const string Description = "some description";

        private Guid dbPositionGuid = Guid.NewGuid();

        private DbCompanyUser dbCompanyUser;
        private CompanyServiceDbContext dbContext;
        private DbPosition dbPosition;
        private IPositionRepository repository;

        private CompanyServiceDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<CompanyServiceDbContext>()
                .UseInMemoryDatabase("InMemoryDatabase")
                .Options;

            return new CompanyServiceDbContext(options);
        }

        [SetUp]
        public void SetUp()
        {
            dbContext = GetMemoryContext();
            repository = new PositionRepository(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        #region GetUserPosition definition

        [Test]
        public void ShouldThrowExceptionWhenUserIdEmpty()
        {
            Assert.Throws<Exception>(() => repository.GetUserPosition(Guid.Empty));
        }

        [Test]
        public void ShouldReturnUserPosition()
        {
            var expected = new DbPosition
            {
                Name = PositionName,
                Description = Description,
                Id = dbPositionGuid
            };

            dbCompanyUser = new DbCompanyUser
            {
                PositionId = dbPositionGuid,
                UserId = Guid.NewGuid()
            };

            dbPosition = new DbPosition
            {
                Id = dbPositionGuid,
                Description = Description,
                Name = PositionName,
                UserIds = new List<DbCompanyUser>{ dbCompanyUser }
            };

            dbContext.Positions.Add(dbPosition);
            dbContext.SaveChanges();

            Assert.AreEqual(expected.Id, repository.GetUserPosition(dbCompanyUser.UserId).Id);
            Assert.AreEqual(expected.Description, repository.GetUserPosition(dbCompanyUser.UserId).Description);
            Assert.AreEqual(expected.Name, repository.GetUserPosition(dbCompanyUser.UserId).Name);

            Assert.That(dbContext.Positions, Is.EquivalentTo(new[] { dbPosition }));
        }

        #endregion
    }
}