using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Repositories;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.CompanyServiceUnitTests.Repositories
{
    public class CompanyRepositoryTests
    {
        private CompanyServiceDbContext dbContext;
        private ICompanyRepository repository;

        private DbCompany dbCompany;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<CompanyServiceDbContext>()
                                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                                    .Options;
            dbContext = new CompanyServiceDbContext(dbOptions);
            repository = new CompanyRepository(dbContext);

            dbCompany = new DbCompany
            {
                Id = Guid.NewGuid(),
                Name = "Lanit-Tercom",
                IsActive = true
            };
        }

        [TearDown]
        public void CleanDb()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

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
