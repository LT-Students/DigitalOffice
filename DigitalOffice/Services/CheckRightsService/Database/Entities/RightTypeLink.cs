using System;

namespace CheckRightsService.Database.Entities
{
    public class RightTypeLink
    {
        public Guid RightId { get; set; }
        public Right Right { get; set; }
        public Guid RightTypeId { get; set; }
        public DbRightType RightType { get; set; }
    }
}
