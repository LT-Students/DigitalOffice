using LT.DigitalOffice.CheckRightsService.Models;
using System.Collections.Generic;

namespace LT.DigitalOffice.CheckRightsService.Commands.Interfaces
{
    public interface IGetRightsListCommand
    {
        List<Right> Execute();
    }
}