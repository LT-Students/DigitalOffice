using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Models;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using System;
using System.Linq;

namespace LT.DigitalOffice.ProjectService.Mappers
{
    public class ProjectMapper : IMapper<DbProject, Project>
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
                WorkersIds = value.WorkersUsersIds?.Select(x => x.WorkerUserId)
            };
        }
    }
}