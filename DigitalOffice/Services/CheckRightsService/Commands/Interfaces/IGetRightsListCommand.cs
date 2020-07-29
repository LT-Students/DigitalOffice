using CheckRightsService.Models;
using System.Collections.Generic;

namespace CheckRightsService.Commands.Interfaces
{
    public interface IGetRightsListCommand
    {
        List<Right> Execute();
    }
}
