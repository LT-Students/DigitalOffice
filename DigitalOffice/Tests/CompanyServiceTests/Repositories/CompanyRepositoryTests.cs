using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Repositories;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using LT.DigitalOffice.CompanyServiceUnitTests.UnitTestLibrary;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.CompanyServiceUnitTests.Repositories
{
    public class CompanyRepositoryTests
    {
        private CompanyServiceDbContext dbContext;
        private ICompanyRepository repository;

        private DbPosition dbPosition;

        private DbCompany dbCompany;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<CompanyServiceDbContext>()
                                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                                    .Options;
            dbContext = new CompanyServiceDbContext(dbOptions);
            repository = new CompanyRepository(dbContext);

            dbPosition = new DbPosition
            {
                Id = Guid.NewGuid(),
                Name = "Position",
                Description = "Description"
            };

            dbCompany = new DbCompany
            {
                Id = Guid.NewGuid(),
                Name = "Lanit-Tercom",
                IsActive = true
            };
        }

        [SetUp]
        public void SetUp()
        {

            dbContext.Positions.Add(dbPosition);
            dbContext.SaveChanges();
        }

        [TearDown]
        public void CleanDb()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        #region GetPositionById
        [Test]
        public void ShouldThrowExceptionIfPositionDoesNotExist()
        {
            Assert.Throws<Exception>(() => repository.GetPositionById(Guid.NewGuid()));
            Assert.AreEqual(dbContext.Positions, new List<DbPosition> { dbPosition });
        }

        [Test]
        public void ShouldReturnSimplePositionInfoSuccessfully()
        {
            var result = repository.GetPositionById(dbPosition.Id);

            var expected = new DbPosition
            {
                Id = dbPosition.Id,
                Name = dbPosition.Name,
                Description = dbPosition.Description
            };

            SerializerAssert.AreEqual(expected, result);
            Assert.AreEqual(dbContext.Positions, new List<DbPosition> { dbPosition });
        }
        #endregion

        #region AddCompany
        [Test]
        public void ShouldReturnMatchingIdAndRightAddCompanyInDb()
        {
            var guidOfNewCompany = repository.AddCompany(dbCompany);

            Assert.AreEqual(dbCompany.Id, guidOfNewCompany);
            Assert.NotNull(dbContext.Companies.Find(dbCompany.Id));
        }
        #endregion
    }
}
