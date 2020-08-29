using System;
using LT.DigitalOffice.Broker.Requests;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.UnitTestLibrary
{
    public class CheckIfUserHaveRightRequest : ICheckIfUserHasRightRequest
    {
        public CheckIfUserHaveRightRequest(int rightId, Guid userId)
        {
            RightId = rightId;
            UserId = userId;
        }

        public int RightId { get; }
        public Guid UserId { get; }
    }
}