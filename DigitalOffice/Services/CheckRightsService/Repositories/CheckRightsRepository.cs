using CheckRightsService.Database;
using CheckRightsService.Database.Entities;
using CheckRightsService.Mappers.Interfaces;
using CheckRightsService.Models;
using CheckRightsService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CheckRightsService.Repositories
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
    }
}
