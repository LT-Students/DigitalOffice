using CheckRightsService.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckRightsService.Database
{
    /// <summary>
    /// Represents database for rights checking.
    /// </summary>
    public class CheckRightsServiceDbContext : DbContext
    {
        public DbSet<Right> Rights { get; set; }

        public DbSet<RightChangeRecord> RightsHistory { get; set; }

        public DbSet<RightProjectLink> RightProjectLinks { get; set; }

        public CheckRightsServiceDbContext(DbContextOptions<CheckRightsServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RightProjectLink>(entity =>
            {
                entity.HasKey(link => new { link.RightId, link.ProjectId });

                entity.HasOne(link => link.Right)
                    .WithMany(r => r.PermissionsIds)
                    .HasForeignKey(link => link.RightId);
            });

            modelBuilder.Entity<RightChangeRecordProjectLink>(entity =>
            {
                entity.HasKey(link => new { link.RightChangeRecordId, link.ProjectId });

                entity.HasOne(link => link.RightChangeRecord)
                    .WithMany(record => record.ChangedPermissionsIds)
                    .HasForeignKey(link => link.RightChangeRecordId);
            });
        }
    }
}
