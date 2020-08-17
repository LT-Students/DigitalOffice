using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.CheckRightsService.RestRequests
{
    public class RightsForUserRequest
    {
        public Guid UserId { get; set; }
        public IEnumerable<int> RightsId { get; set; }
    }
}