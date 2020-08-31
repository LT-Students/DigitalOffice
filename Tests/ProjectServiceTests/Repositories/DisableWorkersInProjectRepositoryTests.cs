using LT.DigitalOffice.Kernel.UnitTestLibrary;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Repositories;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Repositories
{
    class DisableWorkersInProjectRepositoryTests
    {
        #region variables declaration
        private IProjectRepository repository;
        private ProjectServiceDbContext dbContext;

        private DbProject newProject;
        private List<Guid> workersIds;
        private List<DbProjectWorkerUser> workersProject;
        private WorkersIdsInProjectRequest workersIdsInProjectRequest;
        #endregion

        #region setup
        private void CreateMemoryContext()
        {
            var dbOptionsProjectService = new DbContextOptionsBuilder<ProjectServiceDbContext>()
                .UseInMemoryDatabase("ProjectServiceTest")
                .Options;

            dbContext = new ProjectServiceDbContext(dbOptionsProjectService);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var projectId = Guid.NewGuid();
            workersIds = new List<Guid>();
            workersProject = new List<DbProjectWorkerUser>();
            workersIdsInProjectRequest = new WorkersIdsInProjectRequest();

            for (int i = 0; i < 3; i++)
            {
                var worker = new DbProjectWorkerUser
                {
                    ProjectId = projectId,
                    WorkerUserId = Guid.NewGuid(),
                    AddedOn = DateTime.Today,
                    RemovedOn = DateTime.Today,
                    IsManager = false,
                    IsActive = true
                };

                workersProject.Add(worker);
                workersIds.Add(worker.WorkerUserId);
            }

            newProject = new DbProject
            {
                Id = projectId,
                Name = "DigitalOffice",
                DepartmentId = Guid.NewGuid(),
                Description = "New project for Lanit-Tercom",
                Deferred = false,
                IsActive = true,
                WorkersUsersIds = new List<DbProjectWorkerUser>()
            };

            newProject.WorkersUsersIds.AddRange(workersProject);
        }

        [SetUp]
        public void SetUp()
        {
            CreateMemoryContext();

            repository = new ProjectRepository(dbContext);

            dbContext.Add(newProject);
            dbContext.SaveChanges();
        }
        #endregion

        #region successful test
        [Test]
        public void ShouldDisableWorkersSuccessfully()
        {
            workersIdsInProjectRequest.ProjectId = newProject.Id;
            workersIdsInProjectRequest.WorkersIds = workersIds;

            repository.DisableWorkersInProject(workersIdsInProjectRequest);

            var project = dbContext.Projects
                .FirstOrDefault(p => p.Id == workersIdsInProjectRequest.ProjectId);

            Assert.Multiple(() =>
            {
                for(int i = 0; i < project.WorkersUsersIds.Count; i++)
                {
                    workersProject[i].Project = null;
                    project.WorkersUsersIds[i].Project = null;

                    Assert.That(project.WorkersUsersIds[i].IsActive, Is.False);
                    SerializerAssert.AreEqual(workersProject[i], project.WorkersUsersIds[i]);
                }
            });
        }
        #endregion

        #region fail tests
        [Test]
        public void ShouldThrowNullReferenceExceptionWhenProjectIdNotFound()
        {
            workersIdsInProjectRequest.ProjectId = Guid.NewGuid();

            Assert.That(() => repository.DisableWorkersInProject(workersIdsInProjectRequest),
                Throws.InstanceOf<NullReferenceException>().And
                .Message.EqualTo("Project with this Id does not exist."));
        }

        [Test]
        public void ShouldThrowNullReferenceExceptionWhenWorkerIdNotFound()
        {
            workersIdsInProjectRequest.ProjectId = newProject.Id;
            workersIdsInProjectRequest.WorkersIds = new List<Guid> { Guid.NewGuid() };

            Assert.That(() => repository.DisableWorkersInProject(workersIdsInProjectRequest),
                Throws.InstanceOf<NullReferenceException>().And
                .Message.EqualTo("Worker with this Id does not exist."));
        }
        #endregion

        [TearDown]
        public void CleanDbMemory()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
