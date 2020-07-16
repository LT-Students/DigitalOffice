using System;

namespace CheckRightsService.Database.Entities
{
    public class RightRecordProjectLink
    {
        public Guid RightChangeRecordId { get; set; }
        public RightChangeRecord RightChangeRecord { get; set; }
        public Guid ProjectId { get; set; }
    }
}
