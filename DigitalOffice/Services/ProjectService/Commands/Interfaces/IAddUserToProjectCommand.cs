using LT.DigitalOffice.ProjectService.Models;

namespace LT.DigitalOffice.ProjectService.Commands.Interfaces
{
    public interface IAddUserToProjectCommand
    {
        bool Execute(AddUserToProjectRequest projectUser);
    }
}
