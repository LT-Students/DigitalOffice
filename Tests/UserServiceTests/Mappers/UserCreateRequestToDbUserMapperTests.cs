﻿using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;
using NUnit.Framework;
using System;
using System.Security.Cryptography;
using System.Text;

namespace LT.DigitalOffice.UserServiceUnitTests.Mappers
{
    public class UserCreateRequestToDbUserMapperTests
    {
        private IMapper<UserCreateRequest, DbUser> mapper;

        [SetUp]
        public void SetUp()
        {
            mapper = new UserMapper();
        }

        [Test]
        public void ShouldReturnNewDbUserWhenDataCorrect()
        {
            var request = new UserCreateRequest
            {
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Email = "Example@gmail.com",
                Status = "Example",
                Password = "Password",
                IsAdmin = false
            };

            var result = mapper.Map(request);

            var user = new DbUser
            {
                Email = "Example@gmail.com",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Status = "Example",
                PasswordHash = Encoding.UTF8.GetString(new SHA512Managed().ComputeHash(
                    Encoding.UTF8.GetBytes(request.Password))),
                AvatarFileId = null,
                IsActive = true,
                IsAdmin = false
            };

            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.MiddleName, result.MiddleName);
            Assert.AreEqual(user.Status, result.Status);
            Assert.AreEqual(user.PasswordHash, result.PasswordHash);
            Assert.AreEqual(user.AvatarFileId, result.AvatarFileId);
            Assert.AreEqual(user.IsActive, result.IsActive);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenRequestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => mapper.Map(null));
        }
    }
}