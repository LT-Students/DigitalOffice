using System;
using System.Linq;
using UserService.Database.Entities;
using UserService.Mappers.Interfaces;
using UserService.Models;

namespace UserService.Mappers
{
    public class UsersMapper : IMapper<DbUser, User>
    {
        public User Map(DbUser dbUser)
        {
            if (dbUser == null)
            {
                throw new ArgumentNullException("dbUser");
            }

            return new User
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                PasswordHash = dbUser.PasswordHash,
                MiddleName = dbUser.MiddleName,
                Status = dbUser.Status,
                AvatarFileId = dbUser.AvatarFileId,

                AchievementsIds = dbUser.AchievementsIds?.Select(a => a.AchievementId) ?? null,
                CertificatesFilesIds = dbUser.CertificatesFilesIds.Select(a => a.CertificateId) ?? null //
            };
        }
    }
}