using CheckRightsService.Database.Entities;
using CheckRightsService.Mappers.Interfaces;
using CheckRightsService.Models;

namespace CheckRightsService.Mappers
{
    public class RightsMapper : IMapper<DbRight, Right>
    {
        public Right Map(DbRight value)
        {
            return new Right
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description
            };
        }
    }
}
