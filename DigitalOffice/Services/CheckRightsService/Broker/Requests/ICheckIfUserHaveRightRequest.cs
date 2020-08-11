using System;

namespace LT.DigitalOffice.Broker.Requests
{
    /// <summary>
    /// Represents request for CheckIfUserHaveRightConsumer in MassTransit logic.
    /// </summary>
    public interface ICheckIfUserHaveRightRequest
    {
        public int RightId { get; }
        public Guid UserId { get; }
    }
}