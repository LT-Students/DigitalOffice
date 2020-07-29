using CheckRightsService.Database.Entities;
using CheckRightsService.Mappers;
using CheckRightsService.Mappers.Interfaces;
using CheckRightsService.Models;
using NUnit.Framework;
using System;

namespace CheckRightsServiceUnitTests.MappersTests
{
    class RightsMapperTests
    {
        private IMapper<DbRight, Right> mapper;

        private const int Id = 0;
        private const string Name = "Right";
        private const string Description = "Allows you everything";

        private DbRight dbRight;

        [SetUp]
        public void Setup()
        {
            mapper = new RightsMapper();
            dbRight = new DbRight
            {
                Id = Id,
                Name = Name,
                Description = Description
            };
        }

        [Test]
        public void ThrowExceptionIfArgumentIsNull()
        {
            Assert.Throws<NullReferenceException>(() => mapper.Map(null));
        }

        [Test]
        public void ReturnRightModelSuccesfully()
        {
            var result = mapper.Map(dbRight);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Right>(result);
            Assert.AreEqual(Id, result.Id);
            Assert.AreEqual(Name, result.Name);
            Assert.AreEqual(Description, result.Description);
        }
    }
}
