using System;
using System.Linq;
using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.CompanyService.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private CompanyServiceDbContext dbContext;

        public PositionRepository([FromServices] CompanyServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DbPosition GetUserPosition(Guid userId)
        {
            var dbCompanyUser = dbContext.CompaniesUsers
                .FirstOrDefault(companyUser => companyUser.UserId == userId);

            if (dbCompanyUser == null)
            {
                throw new Exception("Position not found.");
            }

            return dbContext.Positions.Find(dbCompanyUser.PositionId);
        }
    }
}
