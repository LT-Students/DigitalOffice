using System;
using NUnit.Framework;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectServiceUnitTests.UnitTestLibrary;

namespace LT.DigitalOffice.ProjectServiceUnitTests.MappersTests
{
    class DbProjectMapperTests
    {
        private NewProjectRequest newRequest;
        private DbProjectMapper mapper;

        [SetUp]
        public void Initialization()
        {
            mapper = new DbProjectMapper();

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
