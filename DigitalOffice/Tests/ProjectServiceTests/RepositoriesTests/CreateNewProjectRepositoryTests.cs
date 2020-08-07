using System;
using NUnit.Framework;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Repositories;
using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.ProjectService.Database.Entities;

namespace LT.DigitalOffice.ProjectServiceUnitTests.RepositoriesTests
{
    class CreateNewProjectRepositoryTests
    {
        private DbProject newProject;
        private ProjectRepository repository;
        private ProjectServiceDbContext dbContext;
        private DbContextOptions<ProjectServiceDbContext> dbOptionsProjectService;

        [SetUp]
        public void Initialization()
        {
            dbOptionsProjectService = new DbContextOptionsBuilder<ProjectServiceDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectServiceTest")
                .Options;

            dbContext = new ProjectServiceDbContext(dbOptionsProjectService);
            repository = new ProjectRepository(dbContext);

            newProject = new DbProject
            {
                Id = Guid.NewGuid(),
                Name = "DigitalOffice",
                DepartmentId = Guid.NewGuid(),
                Description = "New project for Lanit-Tercom",
                Deferred = false,
                IsActive = true
            };

            repository.CreateNewProject(newProject);
        }

        [Test]
        public void FailAddNewProjectInDbMatchOfNameTest()
        {
            Assert.Throws<Exception>(
                () => repository.CreateNewProject(newProject),
                "Project name is already taken.");
        }

        [Test]
        public void SuccessfulAddNewProjectInDbTest()
        {
            newProject.Name = "Any name";
            newProject.Id = Guid.NewGuid();

            Assert.AreEqual(newProject.Id, repository.CreateNewProject(newProject));
        }

        [TearDown]
        public void CleanMemoryDb()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
