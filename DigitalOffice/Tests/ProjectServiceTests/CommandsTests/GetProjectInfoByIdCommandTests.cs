using Moq;
using NUnit.Framework;
using LT.DigitalOffice.ProjectService.Commands;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using LT.DigitalOffice.ProjectServiceUnitTests.UnitTestLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ProjectServiceUnitTests.CommandsTests
{
    public class GetProjectInfoByIdCommandTests
    {
        private IGetProjectInfoByIdCommand command;
        private Mock<IProjectRepository> repositoryMock;
        private Mock<IMapper<DbProject, Project>> mapperMock;

        private DbProjectManagerUser dbManagersIds;
        private DbProjectWorkerUser dbWorkersIds;
        private DbProject project;

        private Guid projectId;
        private Guid workerId;
        private Guid managerId;

        private string Name = "Project";

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IProjectRepository>();
            mapperMock = new Mock<IMapper<DbProject, Project>>();
            command = new GetProjectInfoByIdCommand(repositoryMock.Object, mapperMock.Object);

            workerId = Guid.NewGuid();
            managerId = Guid.NewGuid();
            projectId = Guid.NewGuid();
            dbManagersIds = new DbProjectManagerUser
            {
                ProjectId = projectId,
                Project = project,
                ManagerUserId = managerId
            };
            dbWorkersIds = new DbProjectWorkerUser
            {
                ProjectId = projectId,
                Project = project,
                WorkerUserId = workerId
            };
            project = new DbProject
            {
                Id = projectId,
                Name = Name,
                WorkersUsersIds = new List<DbProjectWorkerUser> { dbWorkersIds },
                ManagersUsersIds = new List<DbProjectManagerUser> { dbManagersIds }
            };
        }

        [Test]
        public void ThrowsExceptionIfRepositoryThrowsIt()
        {
            repositoryMock.Setup(x => x.GetProjectInfoById(It.IsAny<Guid>())).Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(projectId));
        }

        [Test]
        public void ThrowsExceptionIfMapperThrowsIt()
        {
            mapperMock.Setup(x => x.Map(It.IsAny<DbProject>())).Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(projectId));
        }

        [Test]
        public void ReturnsProjectInfoSuccessfully()
        {
            var expected = new Project
            {
                Name = project.Name,
                ManagersIds = project.ManagersUsersIds?.Select(x => x.ManagerUserId),
                WorkersIds = project.WorkersUsersIds?.Select(x => x.WorkerUserId)
            };

            repositoryMock
                .Setup(x => x.GetProjectInfoById(It.IsAny<Guid>()))
                .Returns(project);
            mapperMock
                .Setup(x => x.Map(It.IsAny<DbProject>()))
                .Returns(expected);

            var result = command.Execute(projectId);

            SerializerAssert.AreEqual(expected, result);
        }
    }
}