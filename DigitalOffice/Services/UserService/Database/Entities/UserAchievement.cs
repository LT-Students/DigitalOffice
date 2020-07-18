using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Entities
{
    public class UserAchievement
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid AchievementId { get; set; }
        public virtual Achievement Achievement { get; set; }

        [Required]
        public DateTime Time { get; set; }
    }

    public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
    {
        public void Configure(EntityTypeBuilder<UserAchievement> builder)
        {
            builder.HasKey(pm => new { pm.UserId, pm.AchievementId });
            builder.HasOne<User>(pm => pm.User)
                .WithMany(p => p.AchievementsIds)
                .HasForeignKey(pm => pm.UserId);
        }
    }
}
