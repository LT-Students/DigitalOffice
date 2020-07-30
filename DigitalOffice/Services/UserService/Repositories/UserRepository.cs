using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using LT.DigitalOffice.UserService.Database;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Repositories.Interfaces;

namespace LT.DigitalOffice.UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext dbContext;

        public UserRepository([FromServices] UserServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool UserCreate(DbUser user)
        {
            if (dbContext.Users.Any(users => user.Email == users.Email))
            {
                throw new Exception("Email is already taken.");
            }

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return true;
        }
    }   
}
