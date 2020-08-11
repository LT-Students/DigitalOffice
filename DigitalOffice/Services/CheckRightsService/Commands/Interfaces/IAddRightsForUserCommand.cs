using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Commands.Interfaces
{
    /// <summary>
    /// Add rights for user
    /// </summary>
    public interface IAddRightsForUserCommand
    {
        bool Execute(RightsForUserRequest request);
    }
}