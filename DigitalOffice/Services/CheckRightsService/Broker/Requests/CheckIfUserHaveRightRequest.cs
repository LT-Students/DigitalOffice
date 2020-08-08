using System;

namespace LT.DigitalOffice.Broker.Requests
{
    public class CheckIfUserHaveRightRequest
    {
        public int RightId { get; set; }
        public Guid UserId { get; set; }
    }
}