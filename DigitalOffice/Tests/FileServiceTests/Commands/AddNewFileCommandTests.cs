﻿using FluentValidation;
using Moq;
using NUnit.Framework;
using System;
using LT.DigitalOffice.FileService.Database.Entities;
using LT.DigitalOffice.FileService.RestRequests;
using LT.DigitalOffice.FileService.Commands;
using LT.DigitalOffice.FileService.Commands.Interfaces;
using LT.DigitalOffice.FileService.Repositories.Interfaces;
using LT.DigitalOffice.FileService.Mappers.Interfaces;

namespace LT.DigitalOffice.FileServiceUnitTests.Commands
{
    public class AddNewFileCommandTests
    {
        private DbFile newFile;
        private FileCreateRequest fileRequest;
        private IAddNewFileCommand command;
        private Mock<IFileRepository> repositoryMock;
        private Mock<IValidator<FileCreateRequest>> validatorMock;
        private Mock<IMapper<FileCreateRequest, DbFile>> mapperMock;

        [SetUp]
        public void Initialization()
        {
            repositoryMock = new Mock<IFileRepository>();
            validatorMock = new Mock<IValidator<FileCreateRequest>>();
            mapperMock = new Mock<IMapper<FileCreateRequest, DbFile>>();

            newFile = new DbFile
            {
                Id = Guid.NewGuid(),
                Content = Convert.FromBase64String("RGlnaXRhbCBPZmA5Y2U="),
                Extension = ".txt",
                IsActive = true,
                Name = "DigitalOfficeTestFile",
                AddedOn = DateTime.UtcNow
            };

            fileRequest = new FileCreateRequest
            {
                Content = "RGlnaXRhbCBPZmA5Y2U=",
                Extension = ".txt",
                Name = "DigitalOfficeTestFile"
            };

            mapperMock
                .Setup(f => f.Map(It.IsAny<FileCreateRequest>()))
                .Returns(newFile);
            command = new AddNewFileCommand(repositoryMock.Object, validatorMock.Object, mapperMock.Object);
        }

        [Test]
        public void SuccessfulAddNewFileTest()
        {
            var fileId = Guid.NewGuid();

            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            repositoryMock
                .Setup(x => x.AddNewFile(It.IsAny<DbFile>()))
                .Returns(fileId);

            Assert.AreEqual(fileId, command.Execute(fileRequest));
            validatorMock.Verify(v => v.Validate(It.IsAny<IValidationContext>()), Times.Once);            
            repositoryMock.Verify(r => r.AddNewFile(newFile), Times.Once);
            mapperMock.Verify(m => m.Map(fileRequest), Times.Once);            
        }

        [Test]
        public void FailedAddNewFileInvalidEncodingTest()
        {
            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(false);

            Assert.Throws<ValidationException>(() =>
                command.Execute(fileRequest), "File content encoding validation error");    
            repositoryMock.Verify(r => r.AddNewFile(newFile), Times.Never);
            mapperMock.Verify(m => m.Map(fileRequest), Times.Never);
        }

        [Test]
        public void FailedAddNewFileDuplicateGuidTest()
        {
            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            repositoryMock
                .Setup(x => x.AddNewFile(It.IsAny<DbFile>()))
                .Throws(new Exception());
            
            Assert.Throws<Exception>(() => command.Execute(fileRequest), "GUID duplicated error");
            validatorMock.Verify(v => v.Validate(It.IsAny<IValidationContext>()), Times.Once);
            repositoryMock.Verify(r => r.AddNewFile(newFile), Times.Once);
            mapperMock.Verify(m => m.Map(fileRequest), Times.Once);
        }

        [Test]
        public void FailedAddNewFileNullRequestTest()
        {
            fileRequest = null;
            Assert.Throws<NullReferenceException>(() => command.Execute(fileRequest), "Request is null");            
        }
    }
}