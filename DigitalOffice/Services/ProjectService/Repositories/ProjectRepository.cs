<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
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
=======
﻿using System;
using System.Linq;
using LT.DigitalOffice.ProjectService.Database;
using Microsoft.AspNetCore.Mvc;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;

namespace LT.DigitalOffice.ProjectService.Repositories
>>>>>>> develop
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectServiceDbContext dbContext;

        public ProjectRepository([FromServices] ProjectServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

<<<<<<< HEAD
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
=======
        public DbProject GetProjectInfoById(Guid projectId)
        {
            var project = dbContext.Projects.FirstOrDefault(project => project.Id == projectId);

            if (project == null)
            {
                throw new Exception("Project with this id was not found.");
            }

            return project;
        }

        public Guid CreateNewProject(DbProject newProject)
        {
            if (dbContext.Projects.Any(projects => projects.Name == newProject.Name))
            {
                throw new Exception("Project name is already taken.");
            }

            dbContext.Projects.Add(newProject);
            dbContext.SaveChanges();

            return newProject.Id;
        }
    }
}
>>>>>>> develop
