using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.CompanyService.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyServiceDbContext dbContext;

        public CompanyRepository(CompanyServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region Company
        public DbCompany GetCompanyById(Guid companyId)
        {
            var dbCompany = dbContext.Companies.FirstOrDefault(x => x.Id == companyId);
            if (dbCompany == null)
            {
                throw new Exception("Company was not found.");
            }

            return dbCompany;
        }

        public Guid AddCompany(DbCompany company)
        {
            dbContext.Companies.Add(company);
            dbContext.SaveChanges();

            return company.Id;
        }

        public bool UpdateCompany(DbCompany company)
        {
            var dbCompany = dbContext.Companies
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == company.Id);

            if (dbCompany == null)
            {
                throw new Exception("Company was not found.");
            }

            dbContext.Companies.Update(company);
            dbContext.SaveChanges();

            return true;
        }
        #endregion

        #region Position
        public DbPosition GetPositionById(Guid positionId)
        {
            var dbPosition = dbContext.Positions.FirstOrDefault(position => position.Id == positionId);

            if (dbPosition == null)
            {
                throw new Exception("Position with this id was not found.");
            }

            return dbPosition;
        }

        public List<DbPosition> GetPositionsList()
        {
            return dbContext.Positions.ToList();
        }

        public Guid AddPosition(DbPosition newPosition)
        {
            dbContext.Positions.Add(newPosition);
            dbContext.SaveChanges();

            return newPosition.Id;
        }

        public bool EditPosition(DbPosition newPosition)
        {
            var dbPosition = dbContext.Positions.FirstOrDefault(position => position.Id == newPosition.Id);
            if (dbPosition == null)
            {
                throw new Exception("Position with this id was not found.");
            }

            dbPosition.Name = newPosition.Name;
            dbPosition.Description = newPosition.Description;
            dbContext.Positions.Update(dbPosition);
            dbContext.SaveChanges();

            return true;
        }
        #endregion
    }
}
