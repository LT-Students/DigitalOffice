using LT.DigitalOffice.CheckRightsService.Commands;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.CommandsTests
{
    public class GetRightsListCommandTests
    {
        private Mock<ICheckRightsRepository> repositoryMock;
        private IGetRightsListCommand command;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<ICheckRightsRepository>();
            command = new GetRightsListCommand(repositoryMock.Object);
            var right = new Right { Id = 0, Name = "Right", Description = "Allows you everything" };
            var rightsList = new List<Right> { right };
            repositoryMock.Setup(repository => repository.GetRightsList()).Returns(rightsList);
        }

        [Test]
        public void GetRightsListCommandSuccess()
        {
            var result = command.Execute();

            Assert.DoesNotThrow(() => command.Execute());
            Assert.IsNotNull(result);
        }
    }
}