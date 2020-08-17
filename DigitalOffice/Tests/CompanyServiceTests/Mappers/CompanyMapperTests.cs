using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Mappers;
using LT.DigitalOffice.CompanyService.Mappers.Interfaces;
using LT.DigitalOffice.CompanyService.Models;
using LT.DigitalOffice.CompanyServiceUnitTests.UnitTestLibrary;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.CompanyServiceUnitTests.Mappers
{
    public class CompanyMapperTests
    {
        private IMapper<AddCompanyRequest, DbCompany> mapper;

        private AddCompanyRequest request;
        private DbCompany expectedDbCompanyWithoutId;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mapper = new CompanyMapper();
        }

        [SetUp]
        public void SetUp()
        {
            request = new AddCompanyRequest
            {
                Name = "Lanit-Tercom"
            };

            expectedDbCompanyWithoutId = new DbCompany
            {
                Name = request.Name,
                IsActive = true
            };
        }

        #region AddCompanyRequest to DbCompany
        [Test]
        public void ShouldThrowArgumentNullExceptionWhenAddCompanyRequestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => mapper.Map(null));
        }

        [Test]
        public void ShouldReturnRightModelWhenAddCompanyRequestIsMapped()
        {
            var dbCompany = mapper.Map(request);
            expectedDbCompanyWithoutId.Id = dbCompany.Id;

            Assert.IsInstanceOf<Guid>(dbCompany.Id);
            SerializerAssert.AreEqual(expectedDbCompanyWithoutId, dbCompany);
        }
        #endregion
    }
}
