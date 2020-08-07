using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using UserService.Database;
using UserService.Database.Entities;
using UserService.Mappers.Interfaces;
using UserService.Models;
using UserService.Repositories.Interfaces;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext dbContext;
        private readonly IMapper<DbUser, User> mapper;

        public UserRepository(
            [FromServices] UserServiceDbContext dbContext, 
            [FromServices] IMapper<DbUser, User> mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public User GetUserByEmail(string email)
        {
            return mapper.Map(dbContext.Users.FirstOrDefault(u => u.Email == email));
        }

        public bool ContainsUserWithId(Guid id)
        {
            return dbContext.Users.FirstOrDefault(u => u.Id == id) != null;
        }
    }
}
