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

        private DbCompany dbCompanyInDb;
        private DbCompany dbCompany;
        private DbPosition dbPositionInDb;
        private DbPosition dbPositionToAdd;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<CompanyServiceDbContext>()
                                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                                    .Options;
            dbContext = new CompanyServiceDbContext(dbOptions);
            repository = new CompanyRepository(dbContext);

            dbPositionToAdd = new DbPosition
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
            dbCompanyInDb = new DbCompany
            {
                Id = Guid.NewGuid(),
                Name = "Lanit-Tercom",
                IsActive = true
            };

            dbPositionInDb = new DbPosition
            {
                Id = Guid.NewGuid(),
                Name = "Position",
                Description = "Description"
            };

            dbContext.Companies.Add(dbCompanyInDb);
            dbContext.Positions.Add(dbPositionInDb);
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

        #region GetCompanyById
        [Test]
        public void ShouldThrowExceptionWhenCompanyDoesNotExist()
        {
            Assert.Throws<Exception>(() => repository.GetCompanyById(Guid.NewGuid()));
        }

        [Test]
        public void ShouldRightGetCompanyById()
        {
            var actualCompany = repository.GetCompanyById(dbCompanyInDb.Id);

            var expectedCompany = dbContext.Companies.Find(dbCompanyInDb.Id);
            SerializerAssert.AreEqual(expectedCompany, actualCompany);
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

        #region GetPositionById
        [Test]
        public void ShouldThrowExceptionIfPositionDoesNotExist()
        {
            Assert.Throws<Exception>(() => repository.GetPositionById(Guid.NewGuid()));
            Assert.AreEqual(dbContext.Positions, new List<DbPosition> { dbPositionInDb });
        }

        [Test]
        public void ShouldReturnSimplePositionInfoSuccessfully()
        {
            var result = repository.GetPositionById(dbPositionInDb.Id);

            var expected = new DbPosition
            {
                Id = dbPositionInDb.Id,
                Name = dbPositionInDb.Name,
                Description = dbPositionInDb.Description
            };

            SerializerAssert.AreEqual(expected, result);
            Assert.AreEqual(dbContext.Positions, new List<DbPosition> { dbPositionInDb });
        }
        #endregion

        #region AddPosition
        [Test]
        public void ShouldAddNewPositionSuccessfully()
        {
            var expected = dbPositionToAdd.Id;

            var result = repository.AddPosition(dbPositionToAdd);

            Assert.AreEqual(expected, result);
            Assert.NotNull(dbContext.Positions.Find(dbPositionToAdd.Id));
        }
        #endregion
    }
}
