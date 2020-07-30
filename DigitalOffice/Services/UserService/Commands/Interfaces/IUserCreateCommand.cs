using UserService.RestRequests;

namespace UserService.Commands.Interfaces
{
    public interface IUserCreateCommand
    {
        bool Execute(UserCreateRequest request);
    }
}
