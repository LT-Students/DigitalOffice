using LT.DigitalOffice.UserService.RestRequests;

namespace LT.DigitalOffice.UserService.Commands.Interfaces
{
    public interface IUserCreateCommand
    {
        bool Execute(UserCreateRequest request);
    }
}
