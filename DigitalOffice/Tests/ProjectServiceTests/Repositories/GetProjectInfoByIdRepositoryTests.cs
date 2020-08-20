using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Repositories;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using LT.DigitalOffice.ProjectServiceUnitTests.UnitTestLibrary;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Repositories
{
    public class GetProjectInfoByIdRepositoryTests
    {
        private IProjectRepository repository;
        private ProjectServiceDbContext dbContext;

        private DbProject dbProject;

        [SetUp]
        public void SetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<ProjectServiceDbContext>()
                                    .UseInMemoryDatabase("InMemoryDatabase")
                                    .Options;
            dbContext = new ProjectServiceDbContext(dbOptions);
            repository = new ProjectRepository(dbContext);

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
        public void ShouldThrowExceptionWhenProjectDoesNotExist()
        {
            Assert.Throws<Exception>(() => repository.GetProjectInfoById(Guid.NewGuid()));
            Assert.That(dbContext.Projects, Is.EquivalentTo(new List<DbProject> {dbProject}));
        }

        [Test]
        public void ShouldReturnProjectInfo()
        {
            var result = repository.GetProjectInfoById(dbProject.Id);

            var expected = new DbProject
            {
                Id = dbProject.Id,
                Name = dbProject.Name
            };

            SerializerAssert.AreEqual(expected, result);
            Assert.That(dbContext.Projects, Is.EquivalentTo(new List<DbProject> {dbProject}));
        }
    }
}