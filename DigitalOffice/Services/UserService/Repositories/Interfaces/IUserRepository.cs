using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User GetUserByEmail(string email);
        public bool ContainsUserWithId(Guid id);
    }
}
=======
using LT.DigitalOffice.UserService.Database.Entities;

namespace LT.DigitalOffice.UserService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository. Provides method for getting user model from database.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Returns user model from UserServiceDb with specified id.
        /// </summary>
        /// <param name="userId">Specified id.</param>
        /// <returns>User mode from UserServiceDb.</returns>
        DbUser GetUserInfoById(Guid userId);

        bool UserCreate(DbUser user);
    }
}
>>>>>>> develop
