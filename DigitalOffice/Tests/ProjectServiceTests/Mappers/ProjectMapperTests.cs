using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectServiceUnitTests.UnitTestLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Mappers
{
    public class GetProjectInfoByIdMappersTests
    {
        private IMapper<DbProject, Project> mapper;

        private Guid projectId;
        private Guid workerId;

        private DbProjectWorkerUser dbWorkersIds;

        private DbProject dbProject;

        [SetUp]
        public void SetUp()
        {
            mapper = new ProjectMapper();
            projectId = Guid.NewGuid();
            workerId = Guid.NewGuid();

            dbWorkersIds = new DbProjectWorkerUser
            {
                ProjectId = projectId,
                Project = dbProject,
                WorkerUserId = workerId
            };
            dbProject = new DbProject
            {
                Id = projectId,
                Name = "Project",
                WorkersUsersIds = new List<DbProjectWorkerUser> { dbWorkersIds }
            };
        }

        [Test]
        public void ThrowsExceptionIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => mapper.Map(null));
        }

        [Test]
        public void ReturnsProjectModelSuccessfully()
        {
            var result = mapper.Map(dbProject);

            var expected = new Project
            {
                Name = dbProject.Name,
                WorkersIds = dbProject.WorkersUsersIds?.Select(x => x.WorkerUserId).ToList()
            };

            SerializerAssert.AreEqual(expected, result);
        }
    }
}