using LT.DigitalOffice.CompanyService.Commands.Interfaces;
using LT.DigitalOffice.CompanyService.Controllers;
using LT.DigitalOffice.CompanyService.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.CompanyServiceUnitTests.Controllers
{
    public class CompanyControllerTests
    {
        private CompanyController companyController;
        private Mock<IGetPositionByIdCommand> commandMock;
        private Position position;
        private Guid positionId;

        [SetUp]
        public void SetUp()
        {
            companyController = new CompanyController();
            commandMock = new Mock<IGetPositionByIdCommand>();

            positionId = Guid.NewGuid();
            position = new Position
            {
                Name = "Position",
                Description = "Description"
            };
        }

        #region GetPositionById
        [Test]
        public void ShouldReturnSameValueAsCommandWhenGettingPositionById()
        {
            commandMock
                .Setup(command => command.Execute(positionId))
                .Returns(position)
                .Verifiable();

            Assert.That(companyController.GetPositionById(commandMock.Object, positionId),
                Is.EqualTo(position));
            commandMock.Verify();
        }

        [Test]
        public void ShouldThrowExceptionWhenCommandThrowsIt()
        {
            commandMock
                .Setup(command => command.Execute(It.IsAny<Guid>()))
                .Throws<Exception>();

            Assert.Throws<Exception>(() => companyController.GetPositionById(commandMock.Object, positionId));
        }
        #endregion
    }
}