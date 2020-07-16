using CheckRightsService.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckRightsService.Database
{
    /// <summary>
    /// Represents database for rights checking.
    /// </summary>
    public class CheckRightsServiceDbContext : DbContext
    {
        public DbSet<DbRightType> RightTypes { get; set; }

        public DbSet<Right> Rights { get; set; }

        public DbSet<RightChangeRecord> RightsHistory { get; set; }

        public CheckRightsServiceDbContext(DbContextOptions<CheckRightsServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RightTypeConfiguration());

            modelBuilder.ApplyConfiguration(new RightProjectLinkConfiguration());

            modelBuilder.ApplyConfiguration(new RightRecordProjectLinkConfiguration());

            modelBuilder.ApplyConfiguration(new RightTypeLinkConfiguration());

            modelBuilder.ApplyConfiguration(new RightChangeRecordTypeLinkConfiguration());
        }
    }
}
