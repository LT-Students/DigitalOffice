using System;

namespace LT.DigitalOffice.Broker.Requests
{
    /// <summary>
    /// Represents request for CheckIfUserHaveRightConsumer in MassTransit logic.
    /// </summary>
    public interface ICheckIfUserHaveRightRequest
    {
        int RightId { get; }
        Guid UserId { get; }
    }
}