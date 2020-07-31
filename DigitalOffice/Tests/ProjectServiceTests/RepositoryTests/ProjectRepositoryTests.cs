using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProjectService.Database;
using ProjectService.Database.Entities;
using ProjectService.Mappers;
using ProjectService.Models;
using ProjectService.Repositories;
using System;
using System.Collections.Generic;

namespace ProjectServiceUnitTests.RepositoryTests
{
    class ProjectRepositoryTests
    {
        ProjectServiceDbContext dbContext;
        ProjectRepository repository;

        private ProjectServiceDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ProjectServiceDbContext>()
            .UseInMemoryDatabase(databaseName: "UserRepositoryLoginTest")
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
                ManagersUsersIds = new List<DbProjectManagerUser>(),
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
        public void ShouldNotAddManagerUserWhenProjectIsNotFound()
        {
            var user = new DbProjectManagerUser();
            var id = new Guid();

            Assert.Throws<Exception>(() => repository.AddUserToProject(user, id));
        }

        [Test]
        public void ShouldNotAddWorkerUserWhenUserAlreadyAdded()
        {
            var user = new DbProjectWorkerUser();
            var id = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91");

            repository.AddUserToProject(user, id);

            Assert.Throws<Exception>(() => repository.AddUserToProject(user, id));
        }

        [Test]
        public void ShouldNotAddManagerUserWhenUserAlreadyAdded()
        {
            var user = new DbProjectManagerUser();
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
        }

        [Test]
        public void ShouldAddManagerUser()
        {
            var user = new DbProjectManagerUser();
            var id = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91");

            repository.AddUserToProject(user, id);
        }

    }
}
