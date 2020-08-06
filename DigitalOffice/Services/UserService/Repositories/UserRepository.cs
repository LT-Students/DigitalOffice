﻿using System;
using System.Linq;
using LT.DigitalOffice.UserService.Database;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Repositories.Interfaces;

namespace LT.DigitalOffice.UserService.Repositories
{
    /// <summary>
    /// Represents interface of repository. Provides method for getting user model from database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext userServiceDbContext;

        /// <summary>
        /// Initialize new instance of <see cref="UserRepository"/> with specified <see cref="UserServiceDbContext"/> and <see cref="IMapper{TIn,TOut}"/>
        /// </summary>
        /// <param name="userServiceDbContext">Specified <see cref="userServiceDbContext"/></param>
        public UserRepository(UserServiceDbContext userServiceDbContext)
        {
            this.userServiceDbContext = userServiceDbContext;
        }

        public bool UserCreate(DbUser user)
        {
            if (userServiceDbContext.Users.Any(users => user.Email == users.Email))
            {
                throw new Exception("Email is already taken.");
            }

            userServiceDbContext.Users.Add(user);
            userServiceDbContext.SaveChanges();

            return true;
        }

        public DbUser GetUserInfoById(Guid userId)
            => userServiceDbContext.Users.FirstOrDefault(dbUser => dbUser.Id == userId) ??
               throw new Exception("User with this id not found.");
    }
}