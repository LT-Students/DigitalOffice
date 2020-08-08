using System;

namespace LT.DigitalOffice.Broker.Requests
{
    public interface ICheckIfUserHaveRightRequest
    {
        public int RightId { get; }
        public Guid UserId { get; }
    }
}