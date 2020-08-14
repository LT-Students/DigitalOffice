using System;

namespace LT.DigitalOffice.ProjectService.Broker.Requests
{
    /// <summary>
    /// Iterface for a request that is sent to check User's existence in USerService.
    /// </summary>
    public interface IUserExistenceRequest
    {
        Guid Id { get; }
    }
}
