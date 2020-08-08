using LT.DigitalOffice.CheckRightsService.Models;
using System.Collections.Generic;
using LT.DigitalOffice.Broker.Requests;

namespace LT.DigitalOffice.CheckRightsService.Repositories.Interfaces
{
    public interface ICheckRightsRepository
    {
        List<Right> GetRightsList();
        bool CheckIfUserHaveRight(ICheckIfUserHaveRightRequest request);
    }
}