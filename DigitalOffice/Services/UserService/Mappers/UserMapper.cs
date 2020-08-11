using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LT.DigitalOffice.UserService.Mappers
{
    public class UserMapper : IMapper<DbUser, User>, IMapper<UserCreateRequest, DbUser>
    {
        public User Map(DbUser dbUser)
        {
            if (dbUser == null)
            {
                throw new ArgumentNullException(nameof(dbUser));
            }

            return new User
            {
                Id = dbUser.Id,
                Achievements = dbUser.AchievementsIds?.Select(dbUserAchievement => new Achievement
                {
                    Id = dbUserAchievement.Achievement.Id,
                    Message = dbUserAchievement.Achievement.Message,
                    PictureFileId = dbUserAchievement.Achievement.PictureFileId
                }).ToList(),
                AvatarId = dbUser.AvatarFileId,
                CertificatesIds = dbUser.CertificatesFilesIds?.Select(x => x.CertificateId).ToList(),
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                MiddleName = dbUser.MiddleName,
                Status = dbUser.Status,
                IsAdmin = dbUser.IsAdmin
            };
        }

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
                IsActive = true,
                IsAdmin = request.IsAdmin
            };
        }
    }
}