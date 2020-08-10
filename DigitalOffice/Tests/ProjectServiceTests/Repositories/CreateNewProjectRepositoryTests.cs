using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Repositories
{
    class CreateNewProjectRepositoryTests
    {
        private ProjectRepository repository;
        private ProjectServiceDbContext dbContext;

        private DbProject newProject;

        [SetUp]
        public void SetUp()
        {
            var dbOptionsProjectService = new DbContextOptionsBuilder<ProjectServiceDbContext>()
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
