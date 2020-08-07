using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Mappers;
using LT.DigitalOffice.CheckRightsService.Mappers.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.MappersTests
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
