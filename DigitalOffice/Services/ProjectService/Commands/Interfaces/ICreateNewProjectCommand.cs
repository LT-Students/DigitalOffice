using System;
using LT.DigitalOffice.ProjectService.Models;

namespace LT.DigitalOffice.ProjectService.Commands.Interfaces
{
    public interface ICreateNewProjectCommand
    {
        Guid Execute(NewProjectRequest request);
    }
}
