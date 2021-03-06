﻿using LT.DigitalOffice.UserService.Models;

namespace LT.DigitalOffice.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for editing an existing user.
    /// </summary>
    public interface IEditUserCommand
    {
        /// <summary>
        ///  Editing an existing user. Returns true if it succeeded to edit a user, otherwise false.
        /// </summary>
        /// <param name="request">User data.</param>
        /// <returns>Whether it was successful to edit.</returns>
        bool Execute(EditUserRequest request);
    }
}
