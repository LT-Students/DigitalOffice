using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LT.DigitalOffice.CompanyService.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyServiceDbContext dbContext;

        public CompanyRepository(CompanyServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Guid AddPosition(DbPosition newPosition)
        {
            dbContext.Positions.Add(newPosition);
            dbContext.SaveChanges();

            return newPosition.Id;
        }
        
        public DbPosition GetPositionById(Guid positionId)
        {
            var dbPosition = dbContext.Positions.FirstOrDefault(position => position.Id == positionId);

            if (dbPosition == null)
            {
                throw new Exception("Position with this id was not found.");
            }

            return dbPosition;
        }

        public Guid AddCompany(DbCompany company)
        {
            dbContext.Companies.Add(company);
            dbContext.SaveChanges();

            return company.Id;
        }
    }
}