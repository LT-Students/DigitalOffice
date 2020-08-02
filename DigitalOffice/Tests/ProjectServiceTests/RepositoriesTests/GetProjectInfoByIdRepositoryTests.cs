using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Repositories;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using LT.DigitalOffice.ProjectServiceUnitTests.UnitTestLibrary;
using System;

namespace LT.DigitalOffice.ProjectServiceUnitTests.RepositoriesTests
{
    public class GetProjectInfoByIdRepositoryTests
    {
        private ProjectServiceDbContext dbContext;
        private IProjectRepository repository;
        private Guid projectId;
        private DbProject dbProject;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<ProjectServiceDbContext>()
                                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                                    .Options;
            dbContext = new ProjectServiceDbContext(dbOptions);
            repository = new ProjectRepository(dbContext);

            projectId = Guid.NewGuid();
            dbProject = new DbProject
            {
                Id = Guid.NewGuid(),
                Name = "Project"
            };

            dbContext.Projects.Add(dbProject);
            dbContext.SaveChanges();
        }

        [TearDown]
        public void Clean()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        [Test]
        public void ThrowsExceptionIfProjectDoesNotExist()
        {
            Assert.Throws<Exception>(() => repository.GetProjectInfoById(Guid.NewGuid()));
        }

        [Test]
        public void ReturnsSimpleProjectInfoSuccessfully()
        {
            var result = repository.GetProjectInfoById(dbProject.Id);

            var expected = new DbProject
            {
                Id = dbProject.Id,
                Name = dbProject.Name
            };

            SerializerAssert.AreEqual(expected, result);
        }
    }
}