using System;
using LT.DigitalOffice.UserService.Models;

namespace LT.DigitalOffice.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern. Provides method for getting user model by id.
    /// </summary>
    public interface IGetUserByIdCommand
    {
        /// <summary>
        /// Returns the user model with the specified id.
        /// </summary>
        /// <param name="userId">Specified id.</param>
        /// <returns>User model with specified id.</returns>
        User Execute(Guid userId);
    }
}