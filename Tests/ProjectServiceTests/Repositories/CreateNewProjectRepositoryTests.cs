using System;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Repositories;
using LT.DigitalOffice.ProjectService.Database.Entities;
using System.Linq;
using LT.DigitalOffice.Kernel.UnitTestLibrary;

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
                .UseInMemoryDatabase("ProjectServiceTest")
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
        public void ShouldAddNewProjectWhenTheNameWasRepeated()
        {
            var newProjectWithRepeatedName = newProject;
            newProjectWithRepeatedName.Id = Guid.NewGuid();

            Assert.That(repository.CreateNewProject(newProject), Is.EqualTo(newProjectWithRepeatedName.Id));
            SerializerAssert.AreEqual(newProjectWithRepeatedName, dbContext.Projects.FirstOrDefault(project => project.Id == newProjectWithRepeatedName.Id));
        }

        [Test]
        public void ShouldAddNewProjectToDb()
        {
            newProject.Name = "Any name";
            newProject.Id = Guid.NewGuid();

            Assert.AreEqual(newProject.Id, repository.CreateNewProject(newProject));
            Assert.That(dbContext.Projects.Find(newProject.Id), Is.EqualTo(newProject));
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