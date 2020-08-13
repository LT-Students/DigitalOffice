using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using System;

namespace LT.DigitalOffice.CompanyService.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyServiceDbContext dbContext;

        public CompanyRepository(CompanyServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Guid AddCompany(DbCompany company)
        {
            dbContext.Companies.Add(company);
            dbContext.SaveChanges();

            return company.Id;
        }
    }
}
