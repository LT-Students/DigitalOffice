using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using System;
using System.Linq;

namespace LT.DigitalOffice.ProjectService.Mappers
{
    public class ProjectMapper : IMapper<DbProject, Project>, IMapper<NewProjectRequest, DbProject>
    {
        public Project Map(DbProject value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Project
            {
                Name = value.Name,
                WorkersIds = value.WorkersUsersIds?.Select(x => x.WorkerUserId).ToList()
            };
        }

        public DbProject Map(NewProjectRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
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