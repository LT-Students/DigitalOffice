using NUnit.Framework;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectServiceUnitTests.UnitTestLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ProjectServiceUnitTests.MappersTests
{
    public class GetProjectInfoByIdMappersTests
    {
        private IMapper<DbProject, Project> mapper;

        private const string Name = "Project";

        public Guid projectId;
        private Guid workerId;
        public Guid DepartmentId;

        private DbProjectWorkerUser dbWorkersIds;

        private DbProject dbProject;

        [SetUp]
        public void Setup()
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
                Name = Name,
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
                WorkersIds = dbProject.WorkersUsersIds?.Select(x => x.WorkerUserId)
            };

            SerializerAssert.AreEqual(expected, result);
        }
    }
}