﻿using System;
using System.Linq;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;
using System.Security.Cryptography;
using System.Text;
using LT.DigitalOffice.Broker.Responses;

namespace LT.DigitalOffice.UserService.Mappers
{
    /// <summary>
    /// Represents mapper. Provides methods for converting an object of <see cref="DbUser"/> type into an object of <see cref="User"/> type according to some rule.
    /// </summary>
    public class UserMapper : IMapper<DbUser, User>, IMapper<DbUser, IUserPositionResponse, object>, IMapper<UserCreateRequest, DbUser>, IMapper<EditUserRequest, DbUser>
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

        public DbUser Map(EditUserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new DbUser
            {
                Id = request.Id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Status = request.Status,
                PasswordHash = Encoding.UTF8.GetString(new SHA512Managed().ComputeHash(
                    Encoding.UTF8.GetBytes(request.Password))),
                AvatarFileId = request.AvatarFileId,
                IsActive = request.IsActive,
                IsAdmin = request.IsAdmin
            };
        }

        public object Map(DbUser user, IUserPositionResponse position)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(User));
            }

            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            return new
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                UserPosition = position
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
                PasswordHash = Encoding.UTF8.GetString(new SHA512Managed().ComputeHash(
                    Encoding.UTF8.GetBytes(request.Password))),
                AvatarFileId = null,
                IsActive = true,
                IsAdmin = request.IsAdmin
            };
        }
    }
}