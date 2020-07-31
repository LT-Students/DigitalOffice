using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProjectService.Commands;
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
            var managerMapper = new ManagerMapper();

            command = new AddUserToProjectCommand(validator, repository, workerMapper, managerMapper);
        }
    }
}
