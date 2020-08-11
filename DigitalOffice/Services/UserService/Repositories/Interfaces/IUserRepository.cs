using LT.DigitalOffice.UserService.Database.Entities;
using System;

namespace LT.DigitalOffice.UserService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of UserService.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Returns the user with the specified id from database.
        /// </summary>
        /// <param name="userId">Specified id of user.</param>
        /// <returns>User with specified id.</returns>
        DbUser GetUserInfoById(Guid userId);

        /// <summary>
        /// Adds new user to the database. Returns whether it was successful to add.
        /// </summary>
        /// <param name="user">User to add.</param>
        /// <returns>Whether it was successful to add</returns>
        bool UserCreate(DbUser user);

        bool UserExists(Guid id);
    }
}

