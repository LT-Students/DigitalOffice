using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Commands.Interfaces
{
    public interface IAddRightsForUserCommand
    {
        bool Execute(RightsForUserRequest request);
    }
}