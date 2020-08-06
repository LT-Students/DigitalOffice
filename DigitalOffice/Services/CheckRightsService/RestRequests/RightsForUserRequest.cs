using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.CheckRightsService.RestRequests
{
    public class RightsForUserRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> RightsId { get; set; }
    }
}