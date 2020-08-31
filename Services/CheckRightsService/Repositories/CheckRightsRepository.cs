using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using System;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.CheckRightsService.Repositories
{
    public class CheckRightsRepository : ICheckRightsRepository
    {
        private readonly CheckRightsServiceDbContext dbContext;

        public CheckRightsRepository(CheckRightsServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<DbRight> GetRightsList()
        {
            return dbContext.Rights.ToList();
        }

        public bool CheckIfUserHasRight(Guid userId, int rightId)
        {
            var rights = dbContext.Rights
                .AsNoTracking()
                .Include(r => r.UserIds);

            if (rights.Any(r => r.UserIds.Select(ru => ru.UserId).Contains(userId)))
            {
                return true;
            }

            throw new Exception("Such user doesn't exist or does not have this right.");
        }
    }
}