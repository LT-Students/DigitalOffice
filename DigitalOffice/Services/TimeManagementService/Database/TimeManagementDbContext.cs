using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.TimeManagementService.Database.Entities;

namespace LT.DigitalOffice.TimeManagementService.Database
{
    /// <summary>
    /// TimeManagementDbContext is the primary class that is responsible for interacting with the database.
    /// </summary>
    public class TimeManagementDbContext : DbContext
    {
        public DbSet<DbLeave> Leaves { get; set; }
        public DbSet<DbWorkTime> WorkTimes { get; set; }

        public TimeManagementDbContext(DbContextOptions<TimeManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}