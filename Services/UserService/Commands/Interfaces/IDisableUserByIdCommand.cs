﻿using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for getting result of operation to disabling user.
    /// </summary>
    public interface IDisableUserByIdCommand
    {
        /// <summary>
        /// The result of operation with boolean value.
        /// </summary>
        /// <param name="userId">Specified id.</param>
        /// /// <param name="requestingUserId">The user made the request.</param>
        Task ExecuteAsync(Guid userId, Guid requestingUserId);
    }
}
