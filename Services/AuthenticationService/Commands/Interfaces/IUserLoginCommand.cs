using System.Threading.Tasks;
using LT.DigitalOffice.AuthenticationService.Models;

namespace LT.DigitalOffice.AuthenticationService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// </summary>
    public interface IUserLoginCommand
    {
        /// <summary>
        ///Method for getting user id and jwt by email and password
        /// </summary>
        /// <param name="request">Request model with user email and password.</param>
        /// <returns>Response model with user id and jwt</returns>
        Task<UserLoginResult> Execute(UserLoginInfoRequest request);
    }
}