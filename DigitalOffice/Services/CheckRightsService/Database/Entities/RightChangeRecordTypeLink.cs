using System;

namespace CheckRightsService.Database.Entities
{
    public class RightChangeRecordTypeLink
    {
        public Guid RightChangeRecordId { get; set; }
        public RightChangeRecord RightChangeRecord { get; set; }
        public Guid RightTypeId { get; set; }
        public DbRightType RightType { get; set; }
    }
}
