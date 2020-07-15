using Microsoft.EntityFrameworkCore;
using TimeManagementService.Database.Entities;

namespace TimeManagementService.Database
{
    /// <summary>
    /// TimeManagementDbContext is the primary class that is responsible for interacting with the database.
    /// </summary>
    public class TimeManagementDbContext : DbContext
    {
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }

        public TimeManagementDbContext(DbContextOptions<TimeManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}