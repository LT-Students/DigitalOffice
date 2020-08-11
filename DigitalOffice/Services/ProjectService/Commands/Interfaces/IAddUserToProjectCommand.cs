using LT.DigitalOffice.ProjectService.Models;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ProjectService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for adding a new user to a project.
    /// </summary>
    public interface IAddUserToProjectCommand
    {
        /// <summary>
        /// Adds a new user to a project. 
        /// </summary>
        /// <param name="request">Request containing the user id & project id.</param>
        /// <returns>Whether the operation was successful or not.</returns>
        Task<bool> Execute(AddUserToProjectRequest request);
    }
}
