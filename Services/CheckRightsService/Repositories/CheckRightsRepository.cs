using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void RemoveRightsFromUser(RemoveRightsFromUserRequest request)
        {
            var userRights = dbContext.RightUsers.Where(ru =>
                ru.UserId == request.UserId && request.RightIds.Contains(ru.RightId));

            dbContext.RightUsers.RemoveRange(userRights);
            dbContext.SaveChanges();
        }

        public bool CheckIfUserHasRight(Guid userId, int rightId)
        {
            var rights = dbContext.Rights
                .AsNoTracking()
                .Include(r => r.RightUsers);

            if (rights.Any(r => r.RightUsers.Select(ru => ru.UserId).Contains(userId)))
            {
                return true;
            }

            throw new Exception("Such user doesn't exist or does not have this right.");
        }
    }
}