using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Mappers;
using LT.DigitalOffice.CompanyService.Mappers.Interfaces;
using LT.DigitalOffice.CompanyService.Models;
using LT.DigitalOffice.CompanyServiceUnitTests.UnitTestLibrary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.CompanyServiceUnitTests.Mappers
{
    class PositionMappersTests
    {
        private IMapper<DbPosition, Position> mapper;

        private DbCompanyUser dbUserIds;
        private DbPosition dbPosition;

        [SetUp]
        public void Setup()
        {
            mapper = new PositionMapper();
            dbUserIds = new DbCompanyUser
            {
                UserId = Guid.NewGuid(),
                CompanyId = Guid.NewGuid(),
                PositionId = Guid.NewGuid(),
                IsActive = true,
                StartTime = new DateTime()
            };
            dbPosition = new DbPosition
            {
                Id = dbUserIds.PositionId,
                Name = "Position",
                Description = "Description",
                UserIds = new List<DbCompanyUser> { dbUserIds }
            };
        }

        [Test]
        public void ShouldThrowExceptionIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => mapper.Map(null));
        }

        [Test]
        public void ShouldReturnPositionModelSuccessfully()
        {
            var result = mapper.Map(dbPosition);

            var expected = new Position
            {
                Name = dbPosition.Name,
                Description = dbPosition.Description,
                UserIds = dbPosition.UserIds?.Select(x => x.UserId).ToList()
            };

            SerializerAssert.AreEqual(expected, result);
        }
    }
}