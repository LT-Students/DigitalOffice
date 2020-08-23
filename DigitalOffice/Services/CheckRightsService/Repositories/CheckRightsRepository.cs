using System;
using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.RestRequests;
using LT.DigitalOffice.Kernel.Exceptions;

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

        public bool AddRightsToUser(RightsForUserRequest request)
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

            return true;
        }
    }
}