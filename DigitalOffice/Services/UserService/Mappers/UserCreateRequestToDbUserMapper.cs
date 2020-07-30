using System;
using System.Security.Cryptography;
using System.Text;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.RestRequests;

namespace LT.DigitalOffice.UserService.Mappers
{
    public class UserCreateRequestToDbUserMapper : IMapper<UserCreateRequest, DbUser>
    {
        public DbUser Map(UserCreateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new DbUser
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Status = request.Status,
                PasswordHash = (new SHA512Managed().ComputeHash(Encoding.Default.GetBytes(request.Password))).ToString(),
                AvatarFileId = null,
                IsActive = true
            };
        }
    }
}
