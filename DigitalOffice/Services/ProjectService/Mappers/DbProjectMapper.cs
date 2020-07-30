using System;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;

namespace LT.DigitalOffice.ProjectService.Mappers
{
    public class DbProjectMapper : IMapper<NewProjectRequest, DbProject>
    {
        public DbProject Map(NewProjectRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Request is null");
            }

            return new DbProject
            {
                Id = Guid.NewGuid(),
                DepartmentId = request.DepartmentId,
                Description = request.Description,
                IsActive = request.IsActive,
                Deferred = false
            };
        }
    }
}
