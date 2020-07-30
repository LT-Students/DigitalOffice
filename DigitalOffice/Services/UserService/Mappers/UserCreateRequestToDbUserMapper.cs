using System;
using System.Security.Cryptography;
using System.Text;
using UserService.Database.Entities;
using UserService.Mappers.Interfaces;
using UserService.RestRequests;

namespace UserService.Mappers
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
