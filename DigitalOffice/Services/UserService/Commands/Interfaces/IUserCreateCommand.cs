using LT.DigitalOffice.UserService.Models;

namespace LT.DigitalOffice.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for adding a new user.
    /// </summary>
    public interface IUserCreateCommand
    {
        /// <summary>
        ///  Adds a new user. Returns true if it succeeded to add a user, otherwise false.
        /// </summary>
        /// <param name="request">User data.</param>
        /// <returns>Whether it was successful to add.</returns>
        bool Execute(UserCreateRequest request);
    }
}
