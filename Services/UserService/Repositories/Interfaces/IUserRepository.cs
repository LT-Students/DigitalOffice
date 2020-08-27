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
        /// <returns>Guid of added user.</returns>
        Guid UserCreate(DbUser user);
      
        /// <summary>
        /// Edit existing user. Returns whether it was successful to edit.
        /// </summary>
        /// <param name="user">User to edit.</param>
        /// <returns>Whether it was successful to edit.</returns>
        bool EditUser(DbUser user);

        /// <summary>
        /// Checks if user exists in the repository.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>Whether user exists in the repository or not.</returns>
        bool UserExists(Guid id);

        /// <summary>
        /// Returns the user with the specified email from database.
        /// </summary>
        /// <param name="userEmail">Specified email of user.</param>
        /// <returns>User model.</returns>
        DbUser GetUserByEmail(string userEmail);
    }
}

