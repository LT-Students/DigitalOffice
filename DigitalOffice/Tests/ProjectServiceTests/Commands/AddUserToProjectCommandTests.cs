using FluentValidation;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.ProjectService.Broker.Requests;
using LT.DigitalOffice.ProjectService.Broker.Responses;
using LT.DigitalOffice.ProjectService.Broker.Senders;
using LT.DigitalOffice.ProjectService.Broker.Senders.Interfaces;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProjectService.Commands;
using ProjectService.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectServiceUnitTests.Commands
{
    [TestFixture]
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

        private ProjectServiceDbContext SetupDb()
        {
            dbContext = GetMemoryContext();
            dbContext.Database.EnsureDeleted();

            dbContext.Projects.Add(new DbProject
            {
                Id = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91"),
                WorkersUsersIds = new List<DbProjectWorkerUser>()
            });

            dbContext.SaveChanges();

            dbContext.Projects.FirstOrDefault().WorkersUsersIds.Add(
                new DbProjectWorkerUser
                {
                    WorkerUserId = new Guid("331d4b44-c7f9-4508-98b2-96a324252dba")
                });

            dbContext.SaveChanges();

            return dbContext;
        }

        private Mock<IRequestClient<IUserExistenceRequest>> requestClientMock;
        private AddUserToProjectCommand command;
        private ISender<Guid, IUserExistenceResponse> sender;
        private ProjectServiceDbContext dbContext;

        [OneTimeSetUp]
        public void Initialization()
        {
            requestClientMock = new Mock<IRequestClient<IUserExistenceRequest>>();

            var dbContext = SetupDb();

            var repository = new ProjectRepository(dbContext);
            var validator = new AddUserToProjectRequestValidator();
            var workerMapper = new ProjectUserMapper();

            var response = new ImplementationResponse(new OperationResult(new UserExistenceResponse(true), new List<string>(), true));

            requestClientMock.Setup(c =>
            c.GetResponse<IOperationResult<IUserExistenceResponse>>(It.IsAny<object>(),
            It.IsAny<CancellationToken>(), It.IsAny<RequestTimeout>()))
                .Returns(Task.FromResult<Response<IOperationResult<IUserExistenceResponse>>>(response));

            sender = new UserExistenceSender(requestClientMock.Object);

            command = new AddUserToProjectCommand(validator, repository, workerMapper, sender);
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

            Assert.IsTrue(command.Execute(request).Result);
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

            Assert.ThrowsAsync<ValidationException>(() => command.Execute(request));
        }

        [Test]
        public void ShouldThrowWhenUserNotFound()
        {
            var response = new ImplementationResponse(new OperationResult(new UserExistenceResponse(false), new List<string>(), true));

            var request = new AddUserToProjectRequest
            {
                ProjectId = new Guid("ff15f706-8409-4464-8ea9-980247bd8b91"),
                UserId = new Guid("9e1ed7b0-da55-4048-9657-b4fe17574b2e"),
                IsManager = false
            };

            requestClientMock.Setup(c =>
            c.GetResponse<IOperationResult<IUserExistenceResponse>>(It.IsAny<object>(),
            It.IsAny<CancellationToken>(), It.IsAny<RequestTimeout>()))
                .Returns(Task.FromResult<Response<IOperationResult<IUserExistenceResponse>>>(response));

            Assert.ThrowsAsync<ArgumentException>(() => command.Execute(request));

            response = new ImplementationResponse(new OperationResult(new UserExistenceResponse(true), new List<string>(), true));

            requestClientMock.Setup(c =>
            c.GetResponse<IOperationResult<IUserExistenceResponse>>(It.IsAny<object>(),
            It.IsAny<CancellationToken>(), It.IsAny<RequestTimeout>()))
                .Returns(Task.FromResult<Response<IOperationResult<IUserExistenceResponse>>>(response));
        }

        [OneTimeTearDown]
        public void ClearDb()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
