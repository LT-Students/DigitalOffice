using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.ProjectService.Database;
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
            var dbProject = dbContext.Projects.FirstOrDefault(project => project.Id == projectId);

            if (dbProject == null)
            {
                throw new Exception("Project with this id was not found.");
            }

            return dbProject;
        }

        public Guid CreateNewProject(DbProject newProject)
        {
            dbContext.Projects.Add(newProject);
            dbContext.SaveChanges();

            return newProject.Id;
        }

        public Guid EditProjectById(DbProject dbProject)
        {
            var projectToEdit = dbContext.Projects
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == dbProject.Id);

            if (projectToEdit == null)
            {
                throw new NullReferenceException("Project with this Id does not exist");
            }

            dbContext.Projects.Update(dbProject);
            dbContext.SaveChanges();

            return dbProject.Id;
        }
    }
}