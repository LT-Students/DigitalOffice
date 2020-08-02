using LT.DigitalOffice.ProjectService.Models;
using System;

namespace LT.DigitalOffice.ProjectService.Commands.Interfaces
{
    public interface IGetProjectInfoByIdCommand
    {
        Project Execute(Guid projectId);
    }
}