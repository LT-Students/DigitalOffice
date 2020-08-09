﻿using LT.DigitalOffice.Broker.Requests;

namespace LT.DigitalOffice.CheckRightsService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern. Provides method for checking if user have right.
    /// </summary>
    public interface ICheckIfUserHaveRightCommand
    {
        /// <summary>
        /// Checks if user with specified id have right with specified id.
        /// </summary>
        /// <param name="request">Request containing specified user ID and specified right ID.</param>
        /// <returns>IOperationResult will be containing true body if user with specified id have right with specified id; otherwise body will be false.</returns>
        bool Execute(ICheckIfUserHaveRightRequest request);
    }
}