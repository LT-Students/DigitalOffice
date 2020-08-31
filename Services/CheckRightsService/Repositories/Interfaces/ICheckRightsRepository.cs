using System;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.Database.Entities;

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
        List<DbRight> GetRightsList();

        /// <summary>
        /// Checks whether the user has the specific right.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="rightId">ID of the right.</param>
        /// <returns>True, if there's a UserId-RightId pair. False otherwise.</returns>
        bool CheckIfUserHasRight(Guid userId, int rightId);
    }
}