using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectServiceUnitTests.UnitTestLibrary;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Mappers
{
    class DbProjectMapperTests
    {
        private ProjectMapper mapper;

        private NewProjectRequest newRequest;

        [SetUp]
        public void SetUp()
        {
            mapper = new ProjectMapper();

            newRequest = new NewProjectRequest
            {
                Name = "DigitalOffice",
                DepartmentId = Guid.NewGuid(),
                Description = "New project for Lanit-Tercom",
                IsActive = true
            };
        }

        [Test]
        public void FailObjectsOfNewProjectMapsRequestIsNullTests()
        {
            newRequest = null;

            Assert.Throws<ArgumentNullException>(() => mapper.Map(newRequest), "Request is null.");
        }

        [Test]
        public void SuccessfulObjectsOfNewProjectMapsTests()
        {
            var newProject = mapper.Map(newRequest);

            var expectedDbProject = new DbProject
            {
                Id = newProject.Id,
                DepartmentId = newRequest.DepartmentId,
                Description = newRequest.Description,
                Deferred = false,
                IsActive = newRequest.IsActive
            };

            Assert.IsInstanceOf<Guid>(newProject.Id);
            SerializerAssert.AreEqual(expectedDbProject, newProject);
        }
    }
}
