using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Controllers;
using LT.DigitalOffice.ProjectService.Models;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Controllers
{
    class ProjectControllerTests
    {
        private ProjectController projectController;
        private Mock<IAddUserToProjectCommand> command;

        [SetUp]
        public void Setup()
        {
            projectController = new ProjectController();

        }

        [Test]
        public void AddUserToProjectTest()
        {
            command = new Mock<IAddUserToProjectCommand>();

            var responseTask = Task.FromResult(true);
            command.Setup(c => c.Execute(It.IsAny<AddUserToProjectRequest>())).Returns(responseTask);

            var request = new AddUserToProjectRequest();

            Assert.DoesNotThrowAsync(() => projectController.AddUserToProject(request, command.Object));
        }
    }
}
