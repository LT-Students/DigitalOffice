using LT.DigitalOffice.FileService.Database;
using LT.DigitalOffice.FileService.Database.Entities;
using LT.DigitalOffice.FileService.Repositories;
using LT.DigitalOffice.FileService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.FileServiceUnitTests.Repositories
{
    public class AddNewFileRepositoryTests
    {
        private DbFile newFile;
        private IFileRepository repository;
        private FileServiceDbContext dbContext;
        private DbContextOptions<FileServiceDbContext> dbOptionsFileService;

        [SetUp]
        public void Initialization()
        {
            dbOptionsFileService = new DbContextOptionsBuilder<FileServiceDbContext>()
                .UseInMemoryDatabase(databaseName: "FileServiceTestDatabase")
                .Options;

            dbContext = new FileServiceDbContext(dbOptionsFileService);
            repository = new FileRepository(dbContext);

            newFile = new DbFile
            {
                Id = Guid.NewGuid(),
                Content = Convert.FromBase64String("RGlnaXRhbCBPZmA5Y2U="),
                Extension = ".txt",
                IsActive = true,
                Name = "DigitalOfficeTestFile"
            };
        }

        [Test]
        public void SuccessfulAddNewFileToDatabaseTest()
        {
            Assert.AreEqual(newFile.Id, repository.AddNewFile(newFile));
            Assert.NotNull(dbContext.Files.Find(newFile.Id));
        }
        
        [Test]
        public void FailedAddNewFileFileAlreadyExistsTest()
        {
            repository.AddNewFile(newFile);            
            Assert.Throws<ArgumentException>(() => repository.AddNewFile(newFile));
            Assert.NotNull(dbContext.Files.Find(newFile.Id));
        }
        
        [TearDown]
        public void CleanInMemoryDatabase()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
