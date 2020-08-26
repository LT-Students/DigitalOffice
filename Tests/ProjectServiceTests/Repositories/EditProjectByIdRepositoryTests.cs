using System;
using System.Linq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.Kernel.UnitTestLibrary;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Repositories;
using LT.DigitalOffice.ProjectService.Database.Entities;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Repositories
{
    public class EditProjectByIdRepositoryTests
    {
        private Guid departmentId;

        private DbProject dbProject;
        private DbProject editProject;

        private ProjectRepository repository;
        private ProjectServiceDbContext dbContext;
        private DbContextOptions<ProjectServiceDbContext> dbOptionsProjectService;

        [SetUp]
        public void SetUp()
        {
            dbOptionsProjectService = new DbContextOptionsBuilder<ProjectServiceDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectServiceTest")
                .Options;

            dbContext = new ProjectServiceDbContext(dbOptionsProjectService);
            repository = new ProjectRepository(dbContext);

            departmentId = Guid.NewGuid();

            dbProject = new DbProject
            {
                Name = "A test name",
                DepartmentId = departmentId,
                Description = "Description",
                IsActive = true,
                Deferred = false
            };

            editProject = new DbProject
            {
                Name = "Is different",
                DepartmentId = Guid.NewGuid(),
                Description = "Is different too",
                IsActive = false,
                Deferred = false
            };
        }

        [Test]
        public void ShouldReturnProjectGuidWhenProjectIsEdited()
        {
            DbProject existingProject;
            DbProject editedProject;

            dbProject.Id = Guid.NewGuid();
            editProject.Id = dbProject.Id;

            dbContext.Add(dbProject);
            dbContext.SaveChanges();
            dbContext.Entry(dbProject).State = EntityState.Detached;
            dbContext.SaveChanges();

            existingProject = dbContext.Projects
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == dbProject.Id);

            repository.EditProjectById(editProject);
            dbContext.Entry(editProject).State = EntityState.Detached;
            dbContext.SaveChanges();

            editedProject = dbContext.Projects
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == editProject.Id);

            Assert.IsNotNull(existingProject);
            Assert.IsNotNull(editedProject);
            Assert.AreEqual(existingProject.Id, editedProject.Id);
            SerializerAssert.AreNotEqual(existingProject, editedProject);
            SerializerAssert.AreEqual(editedProject, editProject);
        }

        [Test]
        public void ShouldThrowNoExceptionsWhenNoChangesMadeToDbProject()
        {
            this.dbProject.Id = Guid.NewGuid();

            editProject.Id = dbProject.Id;
            editProject.Name = dbProject.Name;
            editProject.IsActive = dbProject.IsActive;
            editProject.Description = dbProject.Description;
            editProject.Deferred = dbProject.Deferred;
            editProject.DepartmentId = dbProject.DepartmentId;

            dbContext.Add(dbProject);
            dbContext.SaveChanges();
            dbContext.Entry(dbProject).State = EntityState.Detached;
            dbContext.SaveChanges();

            var existingProject = dbContext.Projects
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == dbProject.Id);
            Assert.IsNotNull(existingProject);

            repository.EditProjectById(editProject);
            dbContext.Entry(editProject).State = EntityState.Detached;
            dbContext.SaveChanges();

            var editedProject = dbContext.Projects
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == editProject.Id);
            Assert.IsNotNull(editedProject);

            Assert.AreEqual(existingProject.Id, editedProject.Id);
            SerializerAssert.AreEqual(existingProject, editedProject);
        }

        [Test]
        public void ShouldThrowNullReferenceExceptionWhenNoGuidIsPassedIn()
        {
            Assert.Throws<NullReferenceException>(() => repository.EditProjectById(editProject));
        }

        [Test]
        public void ShouldThrowNullReferenceExceptionWhenGuidIsNull()
        {
            Assert.Throws<NullReferenceException>(() => repository.EditProjectById(editProject));
        }

        [TearDown]
        public void CleanMemoryDb()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}