using NUnit.Framework;
using System;
using System.Security.Cryptography;
using System.Text;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers;
using LT.DigitalOffice.UserService.RestRequests;

namespace LT.DigitalOffice.UserServiceUnitTests.Mappers
{
    class UserCreateRequestToDbUserMapperTests
    {
        private UserCreateRequestToDbUserMapper mapper;
        [SetUp]
        public void Initialization()
        {
            mapper = new UserCreateRequestToDbUserMapper();
        }

        [Test]
        public void ShouldReturnNewDbUserWhenDataCorrect()
        {
            var request = new UserCreateRequest()
            {
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Email = "Example@gmail.com",
                Status = "Example",
                Password = "Example"
            };

            var result = mapper.Map(request);

            var user = new DbUser()
            {
                Email = "Example@gmail.com",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Status = "Example",
                PasswordHash = (new SHA512Managed().ComputeHash(Encoding.Default.GetBytes("Example"))).ToString(),
                AvatarFileId = null,
                IsActive = true
            };
            
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.MiddleName, result.MiddleName);
            Assert.AreEqual(user.Status, result.Status);
            Assert.AreEqual(user.PasswordHash, result.PasswordHash);
            Assert.AreEqual(user.AvatarFileId, result.AvatarFileId);
            Assert.AreEqual(user.IsActive, result.IsActive);
        }

        [Test]
        public void ShouldThrowExceptionWhenRequestIsNull()
        {
            var request = new UserCreateRequest();
            Assert.Throws<ArgumentNullException>(() => mapper.Map(request));
        }
    }
}
