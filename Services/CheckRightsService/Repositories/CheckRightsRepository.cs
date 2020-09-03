using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.Kernel.Exceptions;
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

        public void AddRightsToUser(AddRightsForUserRequest request)
        {
            foreach (var rightId in request.RightsIds)
            {
                var dbRight = dbContext.Rights.FirstOrDefault(right => right.Id == rightId);

                if (dbRight == null)
                {
                    throw new BadRequestException("Right doesn't exist.");
                }

                var dbRightUser = dbContext.RightsUsers.FirstOrDefault(rightUser =>
                    rightUser.RightId == rightId && rightUser.UserId == request.UserId);

                if (dbRightUser == null)
                {
                    dbContext.RightsUsers.Add(new DbRightUser
                    {
                        UserId = request.UserId,
                        Right = dbRight,
                        RightId = rightId,
                    });
                }
            }

            dbContext.SaveChanges();
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