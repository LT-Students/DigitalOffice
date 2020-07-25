using System.Reflection;
using CompanyService.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Database
{
    /// <summary>
    /// A class that defines the tables and its properties in the database.
    /// For this particular case, it defines the database for the ProjectService.
    /// </summary>
    public class CompanyServiceDbContext : DbContext
    {
        public CompanyServiceDbContext (DbContextOptions<CompanyServiceDbContext> options)
            :base(options)
        {
        }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }

        // Fluent API is written here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}