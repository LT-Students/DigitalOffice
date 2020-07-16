using Microsoft.EntityFrameworkCore;
using UserService.Database.Entities;

namespace UserService.Database
{
    /// <summary>
    /// A class that defines the tables and its properties in the database.
    /// For this particular case, it defines the database for the UserService.
    /// </summary>
    public class UserServiceDbContext : DbContext
    {
        public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
            : base(options)
        {
        }

        // Main table.
        public DbSet<User> Users { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Company> Companies { get; set; }

        // Auxiliary table.

        // Fluent API is written here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAchievement>(entity =>
            {
                entity.HasKey(pm => new { pm.UserId, pm.AchievementId });

                entity.HasOne<User>(pm => pm.User)
                    .WithMany(p => p.AchievementsIds)
                    .HasForeignKey(pm => pm.UserId);
            });

            modelBuilder.Entity<UserCertificateFile>(entity =>
            {
                entity.HasKey(pm => new { pm.UserId, pm.CertificateId });

                entity.HasOne<User>(pm => pm.User)
                    .WithMany(p => p.CertificatesFilesIds)
                    .HasForeignKey(pm => pm.UserId);
            });

            modelBuilder.Entity<UserPosition>(entity =>
            {
                entity.HasKey(pm => new { pm.UserId, pm.PositionId });

                entity.HasOne<User>(pm => pm.User)
                    .WithMany(p => p.PositionsIds)
                    .HasForeignKey(pm => pm.UserId);
            });
        }
    }
}