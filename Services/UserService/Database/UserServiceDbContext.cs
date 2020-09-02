using LT.DigitalOffice.UserService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LT.DigitalOffice.UserService.Database
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

        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbAchievement> Achievements { get; set; }

        // Fluent API is written here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}