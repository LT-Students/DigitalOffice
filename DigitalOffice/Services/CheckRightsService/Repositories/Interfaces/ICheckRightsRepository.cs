using System;
using LT.DigitalOffice.CheckRightsService.Models;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository. Provides method for rights.
    /// </summary>
    public interface ICheckRightsRepository
    {
        List<Right> GetRightsList();

        /// <summary>
        /// Adds rights for user
        /// </summary>
        /// <param name="request">Request with rights and user id</param>
        /// <returns>Return true if successfully else return false</returns>
        bool AddRightsToUser(RightsForUserRequest request);
    }
}