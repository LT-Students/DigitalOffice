using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Mappers.Interfaces;
using LT.DigitalOffice.CompanyService.Models;
using System;
using System.Linq;

namespace LT.DigitalOffice.CompanyService.Mappers
{
    public class PositionMapper : IMapper<DbPosition, Position>
    {
        public Position Map(DbPosition value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Position
            {
                Name = value.Name,
                Description = value.Description,
                UserIds = value.UserIds?.Select(x => x.UserId).ToList()
            };
        }
    }
}