﻿using System.Collections.Generic;
using LT.DigitalOffice.Broker.Requests;
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
        /// Checks if user with specified id has right with specified id in database.
        /// </summary>
        /// <param name="request">Request containing specified user ID and specified right ID.</param>
        /// <returns>True if user have right; otherwise false.</returns>
        bool CheckIfUserHasRight(ICheckIfUserHasRightRequest request);
    }
}