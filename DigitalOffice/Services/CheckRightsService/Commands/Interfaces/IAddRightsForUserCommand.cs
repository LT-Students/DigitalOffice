using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Commands.Interfaces
{
    /// <summary>
    /// Add rights for user
    /// </summary>
    public interface IAddRightsForUserCommand
    {
        /// <summary>
        /// Add rights for user
        /// </summary>
        /// <param name="request">Request with rights and user id</param>
        /// <returns>Return true if successfully else return false</returns>
        bool Execute(RightsForUserRequest request);
    }
}