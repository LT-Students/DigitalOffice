using System;
using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Mappers.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Repositories
{
    public class CheckRightsRepository : ICheckRightsRepository
    {
        private readonly CheckRightsServiceDbContext dbContext;
        private readonly IMapper<DbRight, Right> mapper;

        public CheckRightsRepository(CheckRightsServiceDbContext dbContext, IMapper<DbRight, Right> mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<Right> GetRightsList()
        {
            return dbContext.Rights.Select(r => mapper.Map(r)).ToList();
        }

        public bool AddRightsToUser(RightsForUserRequest request)
        {
            foreach (var rightId in request.RightsId)
            {
                var dbRight = dbContext.Rights.FirstOrDefault(right => right.Id == rightId);
                if (dbRight == null)
                {
                    //TODO add custom exception
                    throw new Exception("Right doesn't exist");
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