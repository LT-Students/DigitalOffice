using CheckRightsService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CheckRightsService.Database
{
    /// <summary>
    /// Represents database for rights checking.
    /// </summary>
    public class CheckRightsServiceDbContext : DbContext
    {
        public DbSet<Right> Rights { get; set; }

        public DbSet<RightChangeRecord> RightsHistory { get; set; }

        public CheckRightsServiceDbContext(DbContextOptions<CheckRightsServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
