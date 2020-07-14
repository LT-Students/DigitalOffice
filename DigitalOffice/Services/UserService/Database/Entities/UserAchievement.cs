using System;

namespace UserService.Database.Entities
{
    public class UserAchievement
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }

        public DateTime Time { get; set; }
    }
}
