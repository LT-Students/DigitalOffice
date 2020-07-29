using CheckRightsService.Models;
using System.Collections.Generic;

namespace CheckRightsService.Repositories.Interfaces
{
    public interface ICheckRightsRepository
    {
        List<Right> GetRightsList();
    }
}