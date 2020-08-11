using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProjectService.Commands;
using ProjectService.Controllers;
using ProjectService.Database;
using ProjectService.Database.Entities;
using ProjectService.Mappers;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using ProjectService.Repositories;
using ProjectService.Repositories.Interfaces;
using ProjectService.Validators;
using System;
using System.Collections.Generic;

namespace ProjectServiceUnitTests.CommandsTest
{
    class AddUserToProjectCommandTests
    {
        AddUserToProjectCommand command;
        ProjectController pc;

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
            var dbContext = GetMemoryContext();
            dbContext.Database.EnsureDeleted();

            dbContext.Projects.Add(new DbProject
            {
                Id = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91"),
                ManagersUsersIds = new List<DbProjectManagerUser>(),
                WorkersUsersIds = new List<DbProjectWorkerUser>()
            });

            dbContext.SaveChanges();

            var repository = new ProjectRepository(dbContext);
            var validator = new AddUserToProjectRequestValidator();
            var workerMapper = new WorkerMapper();
            var managerMapper = new ProjectUserMapper();

            command = new AddUserToProjectCommand(validator, repository, workerMapper, managerMapper);

            pc = new ProjectController();
        }

        [Test]
        public void ShouldAddManagerTest()
        {
            var request = new AddUserToProjectRequest
            {
                ProjectId = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91"),
                UserId = new Guid("72a92185-cf55-41c2-b339-4535d8b38340"),
                IsManager = true
            };

            Assert.IsTrue(pc.AdduserToProject(request, command));
        }

        [Test]
        public void ShouldAddWorkerTest()
        {
            var request = new AddUserToProjectRequest
            {
                ProjectId = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91"),
                UserId = new Guid("72a92185-cf55-41c2-b339-4535d8b38340"),
                IsManager = false
            };

            Assert.IsTrue(pc.AdduserToProject(request, command));
        }

        [Test]
        public void ShouldThrowOnValidationFail()
        {
            var request = new AddUserToProjectRequest
            {
                ProjectId = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91"),
                UserId = new Guid(),
                IsManager = true
            };

            Assert.Throws<ValidationException>(() => pc.AdduserToProject(request, command));
        }

        [Test]
        public void ShouldThrowWhenUserNotFound()
        {
            // Will be implemented when communication with UserService is done
        }
    }
}
