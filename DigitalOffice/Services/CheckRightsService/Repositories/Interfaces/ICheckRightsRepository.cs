using LT.DigitalOffice.CheckRightsService.Models;
using System.Collections.Generic;
using LT.DigitalOffice.Broker.Requests;

namespace LT.DigitalOffice.CheckRightsService.Repositories.Interfaces
{
    public interface ICheckRightsRepository
    {
        List<Right> GetRightsList();
        
        /// <summary>
        /// Checks if user with specified id have right with specified id in database.
        /// </summary>
        /// <param name="request">Request containing specified user ID and specified right ID.</param>
        /// <returns>True if user have right; otherwise false.</returns>
        bool CheckIfUserHaveRight(ICheckIfUserHaveRightRequest request);
    }
}