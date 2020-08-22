using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Repositories;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using LT.DigitalOffice.CompanyServiceUnitTests.UnitTestLibrary;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.CompanyServiceUnitTests.Repositories
{
    public class CompanyRepositoryTests
    {
        private CompanyServiceDbContext dbContext;
        private ICompanyRepository repository;

        private DbPosition dbPosition;
        private DbPosition newPosition;
        private Guid positionId;
        private DbPosition dbPositionToAdd;
        private DbCompany dbCompanyInDb;
        private DbCompany dbCompany;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<CompanyServiceDbContext>()
                                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                                    .Options;
            dbContext = new CompanyServiceDbContext(dbOptions);

            repository = new CompanyRepository(dbContext);
        }

        [SetUp]
        public void SetUp()
        {
            positionId = Guid.NewGuid();
            dbPosition = new DbPosition
            {
                Id = positionId,
                Name = "Name",
                Description = "Description"
            };
            dbContext.Positions.Add(dbPosition);
            dbContext.SaveChanges();

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

            dbCompanyInDb = new DbCompany
            {
                Id = Guid.NewGuid(),
                Name = "Lanit-Tercom",
                IsActive = true
            };

            dbContext.Companies.Add(dbCompanyInDb);
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

        #region EditPosition
        [Test]
        public void ShouldEditPositionSuccessfully()
        {
            newPosition = new DbPosition
            {
                Id = positionId,
                Name = "abracadabra",
                Description = "bluhbluh"
            };

            var result = repository.EditPosition(newPosition);
            DbPosition updatedPosition = dbContext.Positions.FirstOrDefault(dbPosition => dbPosition.Id == positionId);

            Assert.IsTrue(result);
            SerializerAssert.AreEqual(newPosition, updatedPosition);
            Assert.That(dbContext.Positions, Is.EquivalentTo(new List<DbPosition> { updatedPosition }));
		    }
        #endregion

        #region GetPositionsList
        [Test]
        public void GetPositionsListSuccessfully()
        {
            var result = repository.GetPositionsList();

            var expected = new List<DbPosition> { dbPosition };

            Assert.DoesNotThrow(() => repository.GetPositionsList());
            SerializerAssert.AreEqual(expected, result);
            Assert.That(dbContext.Positions, Is.EqualTo(new List<DbPosition> { dbPosition }));
        }
        #endregion

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