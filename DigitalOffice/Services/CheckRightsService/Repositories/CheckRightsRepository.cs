using System;
using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.RestRequests;

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
            foreach (var rightId in request.RightsId)
            {
                var dbRight = dbContext.Rights.FirstOrDefault(right => right.Id == rightId);
                if (dbRight == null)
                {
                    //TODO change on custom exception
                    throw new Exception("Right doesn't exist.");
                }

                dbRight.UserIds.Add(new DbRightUser
                {
                    UserId = request.UserId,
                    RightId = rightId,
                });

                dbContext.Rights.Update(dbRight);
            }

            dbContext.SaveChanges();

            return true;
        }
    }
}