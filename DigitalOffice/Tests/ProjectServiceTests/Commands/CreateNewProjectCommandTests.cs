using FluentValidation;
using LT.DigitalOffice.ProjectService.Commands;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Commands
{
    class CreateNewProjectCommandTests
    {
        private ICreateNewProjectCommand command;
        private Mock<IProjectRepository> repositoryMock;
        private Mock<IValidator<NewProjectRequest>> validatorMock;
        private Mock<IMapper<NewProjectRequest, DbProject>> mapperMock;

        private DbProject newProject;
        private NewProjectRequest newRequest;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            validatorMock = new Mock<IValidator<NewProjectRequest>>();
            repositoryMock = new Mock<IProjectRepository>();
            mapperMock = new Mock<IMapper<NewProjectRequest, DbProject>>();

            command = new CreateNewProjectCommand(repositoryMock.Object, validatorMock.Object, mapperMock.Object);

            newRequest = new NewProjectRequest
            {
                Name = "DigitalOffice",
                DepartmentId = Guid.NewGuid(),
                Description = "New project for Lanit-Tercom",
                IsActive = true
            };

            newProject = new DbProject
            {
                Id = Guid.NewGuid(),
                Name = newRequest.Name,
                DepartmentId = newRequest.DepartmentId,
                Description = newRequest.Description,
                Deferred = false,
                IsActive = newRequest.IsActive,
            };
        }

        [Test]
        public void FailCreateNewProjectIncorrectProjectDataTests()
        {
            validatorMock
                .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(false);

            Assert.Throws<ValidationException>(() => command.Execute(newRequest), "Project field validation error");
        }

        [Test]
        public void FailCreateNewProjectMatchOfProjectNameTests()
        {
            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            mapperMock
                .Setup(x => x.Map(It.IsAny<NewProjectRequest>()))
                .Returns(newProject);

            repositoryMock
                .Setup(x => x.CreateNewProject(It.IsAny<DbProject>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(newRequest), "Project name is already taken.");
        }

        [Test]
        public void FailCreateNewProjectRequestIsNullTests()
        {
            newRequest = null;

            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            mapperMock
                .Setup(x => x.Map(It.IsAny<NewProjectRequest>()))
                .Returns(newProject);

            repositoryMock
                .Setup(x => x.CreateNewProject(It.IsAny<DbProject>()))
                .Throws(new ArgumentNullException());

            Assert.Throws<ArgumentNullException>(() => command.Execute(newRequest), "Request is null.");
        }

        [Test]
        public void SuccessfulCreateNewProjectTest()
        {
            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            mapperMock
                .Setup(x => x.Map(It.IsAny<NewProjectRequest>()))
                .Returns(newProject);

            repositoryMock
                .Setup(x => x.CreateNewProject(It.IsAny<DbProject>()))
                .Returns(newProject.Id);

            Assert.AreEqual(newProject.Id, command.Execute(newRequest));
        }
    }
}
