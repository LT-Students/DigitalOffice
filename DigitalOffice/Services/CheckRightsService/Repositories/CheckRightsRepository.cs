using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Mappers.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.Broker.Requests;

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

        public bool CheckIfUserHasRight(ICheckIfUserHasRightRequest request)
            => dbContext.Rights.Where(right => right.Id == request.RightId)
                .Any(right => right.UserIds.Select(rightUser => rightUser.UserId).Contains(request.UserId));
    }
}