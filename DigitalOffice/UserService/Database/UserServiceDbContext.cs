﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<User> Users { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Company> Companies { get; set; }

        // Fluent API is written here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}