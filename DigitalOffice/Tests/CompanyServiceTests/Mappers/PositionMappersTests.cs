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
        private IMapper<AddPositionRequest, DbPosition> mapperAddPositionRequest;
        private IMapper<DbPosition, Position> mapper;

        private DbCompanyUser dbUserIds;
        private DbPosition dbPosition;

        private AddPositionRequest request;
        private DbPosition expectedDbPosition;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mapper = new PositionMapper();
            mapperAddPositionRequest = new PositionMapper();
        }

        [SetUp]
        public void SetUp()
        {
            request = new AddPositionRequest
            {
                Name = "Name",
                Description = "Description"
            };
            expectedDbPosition = new DbPosition
            {
                Name = request.Name,
                Description = request.Description
            };

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

        #region AddPositionRequest to DbPosition
        [Test]
        public void ShouldThrowExceptionIfArgumentIsNullAddPositionRequestToDbPosition()
        {
            Assert.Throws<ArgumentNullException>(() => mapperAddPositionRequest.Map(null));
        }

        public void ShouldMapAddPositionRequestToDbPositionSuccessfully()
        {
            var resultDbPosition = mapperAddPositionRequest.Map(request);

            expectedDbPosition.Id = resultDbPosition.Id;

            Assert.IsInstanceOf<Guid>(resultDbPosition.Id);
            SerializerAssert.AreEqual(expectedDbPosition, resultDbPosition);
        }
        #endregion

        #region DbPosition to Position
        [Test]
        public void ShouldThrowExceptionIfArgumentIsNullDbPositionToPosition()
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
        #endregion
    }
}