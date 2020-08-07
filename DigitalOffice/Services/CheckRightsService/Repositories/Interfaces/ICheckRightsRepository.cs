using System;
using LT.DigitalOffice.CheckRightsService.Models;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Repositories.Interfaces
{
    public interface ICheckRightsRepository
    {
        List<Right> GetRightsList();
        bool AddRightsToUser(RightsForUserRequest userId);
    }
}