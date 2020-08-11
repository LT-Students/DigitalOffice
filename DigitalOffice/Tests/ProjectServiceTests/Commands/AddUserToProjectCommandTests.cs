using FluentValidation;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.ProjectService.Broker.Requests;
using LT.DigitalOffice.ProjectService.Broker.Responses;
using LT.DigitalOffice.ProjectService.Broker.Senders;
using LT.DigitalOffice.ProjectService.Controllers;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Repositories;
using MassTransit;
using MassTransit.Clients;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProjectService.Commands;
using ProjectService.Validators;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ProjectServiceUnitTests.Commands
{
    class AddUserToProjectCommandTests
    {
        
        private class ImplementationResponse : Response<IOperationResult<IUserExistenceResponse>>
        {
            public Guid? MessageId { get; }
            public Guid? RequestId { get; }
            public Guid? CorrelationId { get; }

            public Guid? ConversationId { get; }

            public Guid? InitiatorId { get; }

            public DateTime? ExpirationTime { get; }

            public Uri SourceAddress { get; }

            public Uri DestinationAddress { get; }

            public Uri ResponseAddress { get; }

            public Uri FaultAddress { get; }

            public DateTime? SentTime { get; }

            public Headers Headers { get; }

            public HostInfo Host { get; }
            public IOperationResult<IUserExistenceResponse> Message { get; }

            public ImplementationResponse(IOperationResult<IUserExistenceResponse> message)
            {
                Message = message;
            }
        }

        private class UserExistenceResponse : IUserExistenceResponse
        {
            public UserExistenceResponse(bool exists)
            {
                Exists = exists;
            }

            public bool Exists { get; }
        }

        private class OperationResult : IOperationResult<IUserExistenceResponse>
        {
            public OperationResult(IUserExistenceResponse body, List<string> errors, bool isSuccess)
            {
                Body = body;
                Errors = errors;
                IsSuccess = isSuccess;
            }

            public bool IsSuccess { get; }
            public List<string> Errors { get; }
            public IUserExistenceResponse Body { get; }
        }

        private ProjectServiceDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ProjectServiceDbContext>()
            .UseInMemoryDatabase(databaseName: "UserRepositoryLoginTest")
            .Options;

            return new ProjectServiceDbContext(options);
        }

        AddUserToProjectCommand command;
        ProjectController pc;

        [SetUp]
        public void Initialization()
        {
            var dbContext = GetMemoryContext();
            dbContext.Database.EnsureDeleted();

            dbContext.Projects.Add(new DbProject
            {
                Id = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91"),
                WorkersUsersIds = new List<DbProjectWorkerUser>()
            });

            dbContext.SaveChanges();

            var repository = new ProjectRepository(dbContext);
            var validator = new AddUserToProjectRequestValidator();
            var workerMapper = new ProjectUserMapper();

            Mock<IRequestClient<IUserExistenceRequest>> requestClientMock;

            var sender = new Mock<UserExistenceSender>();

            command = new AddUserToProjectCommand(validator, repository, workerMapper, sender.Object);

            pc = new ProjectController();
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
}
