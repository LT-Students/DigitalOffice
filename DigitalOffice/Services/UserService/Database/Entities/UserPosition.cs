using System;

namespace UserService.Database.Entities
{
    public class UserPosition
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid PositionId { get; set; }
        public virtual Position Position { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
