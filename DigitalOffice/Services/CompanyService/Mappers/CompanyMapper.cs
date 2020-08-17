using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Mappers.Interfaces;
using LT.DigitalOffice.CompanyService.Models;
using System;

namespace LT.DigitalOffice.CompanyService.Mappers
{
    public class CompanyMapper : IMapper<AddCompanyRequest, DbCompany>
    {
        public DbCompany Map(AddCompanyRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new DbCompany
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                IsActive = true
            };
        }
    }
}
