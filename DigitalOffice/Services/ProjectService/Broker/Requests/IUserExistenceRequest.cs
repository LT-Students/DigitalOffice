using System;

namespace LT.DigitalOffice.ProjectService.Broker.Requests
{
    /// <summary>
    /// Interface for a request that is sent to check User's existence in UserService.
    /// </summary>
    public interface IUserExistenceRequest
    {
        Guid Id { get; }
    }
}
