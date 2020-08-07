using LT.DigitalOffice.CheckRightsService.Models;
using System.Collections.Generic;

namespace LT.DigitalOffice.CheckRightsService.Repositories.Interfaces
{
    public interface ICheckRightsRepository
    {
        List<Right> GetRightsList();
    }
}