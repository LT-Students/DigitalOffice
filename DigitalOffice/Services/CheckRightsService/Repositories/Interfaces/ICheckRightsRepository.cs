using LT.DigitalOffice.CheckRightsService.Models;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of CheckRightsService.
    /// </summary>
    public interface ICheckRightsRepository
    {
        /// <summary>
        /// Returns a list of all added rights to the database.
        /// </summary>
        /// <returns>List of all added rights.</returns>
        List<Right> GetRightsList();

        /// <summary>
        /// Adds rights for user
        /// </summary>
        /// <param name="request">Request with rights and user id</param>
        /// <returns>Return true if successfully else return false</returns>
        bool AddRightsToUser(RightsForUserRequest request);
    }
}