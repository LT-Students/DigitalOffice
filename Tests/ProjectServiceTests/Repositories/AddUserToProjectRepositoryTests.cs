using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Repositories
{
    class AddUserToProjectRepositoryTests
    {
        ProjectServiceDbContext dbContext;
        ProjectRepository repository;

        private ProjectServiceDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ProjectServiceDbContext>()
            .UseInMemoryDatabase(databaseName: "AddUserToProjectTest")
            .Options;

            return new ProjectServiceDbContext(options);
        }

        [SetUp]
        public void Initialization()
        {
            dbContext = GetMemoryContext();
            dbContext.Database.EnsureDeleted();

            dbContext.Projects.Add(new DbProject
            {
                Id = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91"),
                WorkersUsersIds = new List<DbProjectWorkerUser>()
            });

            dbContext.SaveChanges();

            repository = new ProjectRepository(dbContext);
        }

        [Test]
        public void ShouldNotAddWorkerUserWhenProjectIsNotFound()
        {
            var user = new DbProjectWorkerUser();
            var id = new Guid();

            Assert.Throws<Exception>(() => repository.AddUserToProject(user, id));
        }

        [Test]
        public void ShouldNotAddWorkerUserWhenUserIsAlreadyAdded()
        {
            var user = new DbProjectWorkerUser();
            var id = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91");

            repository.AddUserToProject(user, id);

            Assert.Throws<Exception>(() => repository.AddUserToProject(user, id));
        }

        [Test]
        public void ShouldAddWorkerUser()
        {
            var user = new DbProjectWorkerUser();
            var id = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91");

            repository.AddUserToProject(user, id);
            Assert.AreEqual(1, dbContext.Projects.FirstOrDefault().WorkersUsersIds.Count);
        }

        [TearDown]
        public void TearDown()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
