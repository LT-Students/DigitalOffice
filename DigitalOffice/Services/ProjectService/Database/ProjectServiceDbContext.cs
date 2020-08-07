using System.Reflection;
using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.ProjectService.Database.Entities;

namespace LT.DigitalOffice.ProjectService.Database
{
    /// <summary>
    /// A class that defines the tables and its properties in the database.
    /// For this particular case, it defines the database for the LT.DigitalOffice.ProjectService.
    /// </summary>
    public class ProjectServiceDbContext : DbContext
    {
        public ProjectServiceDbContext (DbContextOptions<ProjectServiceDbContext> options)
            :base(options)
        {
        }

        public DbSet<DbProject> Projects { get; set; }

        // Fluent API is written here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}