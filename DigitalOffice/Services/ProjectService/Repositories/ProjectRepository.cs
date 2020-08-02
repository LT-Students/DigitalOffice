using System;
using System.Linq;
using LT.DigitalOffice.ProjectService.Database;
using Microsoft.AspNetCore.Mvc;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;

namespace LT.DigitalOffice.ProjectService.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectServiceDbContext dbContext;

        public ProjectRepository([FromServices] ProjectServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

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