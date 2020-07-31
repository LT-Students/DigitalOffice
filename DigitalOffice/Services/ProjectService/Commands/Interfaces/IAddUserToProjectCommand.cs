using ProjectService.Models;

namespace ProjectService.Commands.Interfaces
{
    public interface IAddUserToProjectCommand
    {
        bool Execute(AddUserToProjectRequest projectUser);
    }
}
