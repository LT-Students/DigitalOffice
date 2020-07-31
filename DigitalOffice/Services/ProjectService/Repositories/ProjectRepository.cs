using Microsoft.AspNetCore.Mvc;
using ProjectService.Commands;
using ProjectService.Database;
using ProjectService.Database.Entities;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using ProjectService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectServiceDbContext dbContext;

        public ProjectRepository([FromServices] ProjectServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddUserToProject(DbProjectWorkerUser user, Guid projectId)
        {
            var project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project == null)
            {
                throw new Exception("Project does not exist.");
            }

            if (project.WorkersUsersIds.Contains(user))
            {
                throw new Exception("User is already in the project.");
            }

            project.WorkersUsersIds.Add(user);
            dbContext.SaveChanges();

            return true;
        }

        public bool AddUserToProject(DbProjectManagerUser user, Guid projectId)
        {
            var project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project == null)
            {
                throw new Exception("Project does not exist.");
            }

            if (project.ManagersUsersIds.Contains(user))
            {
                throw new Exception("User is already in the project.");
            }

            project.ManagersUsersIds.Add(user);
            dbContext.SaveChanges();

            return true;
        }
    }
}
