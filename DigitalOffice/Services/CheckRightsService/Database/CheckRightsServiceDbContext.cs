using CheckRightsService.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckRightsService.Database
{
    public class CheckRightsServiceDbContext : DbContext
    {
        public DbSet<Right> Rights { get; set; }

        public CheckRightsServiceDbContext(DbContextOptions<CheckRightsServiceDbContext> options) : base(options) { }
    }
}
