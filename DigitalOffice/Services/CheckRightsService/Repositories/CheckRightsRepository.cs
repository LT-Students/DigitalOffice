using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.CheckRightsService.Database.Entities;

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
    }
}