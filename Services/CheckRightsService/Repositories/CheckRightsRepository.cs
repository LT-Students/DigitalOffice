using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.Broker.Requests;

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

        public bool CheckIfUserHasRight(ICheckIfUserHasRightRequest request)
            => dbContext.Rights.Where(right => right.Id == request.RightId)
                .Any(right => right.UserIds.Select(rightUser => rightUser.UserId).Contains(request.UserId));
    }
}