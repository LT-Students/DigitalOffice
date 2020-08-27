using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Controllers;
using LT.DigitalOffice.ProjectService.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Controllers
{
    class ProjectControllerTests
    {
        private ProjectController projectController;
        private Mock<IAddUsersToProjectCommand> command;

        [SetUp]
        public void Setup()
        {
            projectController = new ProjectController();

        }

        [Test]
        public void AddUserToProjectTest()
        {
            command = new Mock<IAddUsersToProjectCommand>();

            var responseTask = Task.FromResult(new List<bool>() { true } as IEnumerable<bool>);
            command.Setup(c => c.Execute(It.IsAny<AddUserToProjectRequest>())).Returns(responseTask);

            var request = new AddUserToProjectRequest();

            Assert.DoesNotThrowAsync(() => projectController.AddUserToProject(request, command.Object));
        }
    }
}
