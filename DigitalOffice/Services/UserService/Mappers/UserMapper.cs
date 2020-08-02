using System;
using System.Linq;
using LT.DigitalOffice.UserService.Database;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;

namespace LT.DigitalOffice.UserService.Mappers
{
    /// <summary>
    /// Represents mapper. Provides methods for converting an object of <see cref="DbUser"/> type into an object of <see cref="User"/> type according to some rule.
    /// </summary>
    public class UserMapper : IMapper<DbUser, User>
    {
        /// <summary>
        /// Convert an object of <see cref="DbUser"/> type into an object of <see cref="User"/> type according to some rule.
        /// </summary>
        /// <param name="dbUser">Incoming model of <see cref="UserServiceDbContext"/>.</param>
        /// <returns>User model for response.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dbUser"/> is null.</exception>
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
                }),
                AvatarId = dbUser.AvatarFileId,
                CertificatesIds = dbUser.CertificatesFilesIds?.Select(x => x.CertificateId),
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                MiddleName = dbUser.MiddleName,
                Status = dbUser.Status,
                IsAdmin = dbUser.IsAdmin
            };
        }
    }
}