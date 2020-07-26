using Microsoft.EntityFrameworkCore;
using FileService.Database.Entities;

namespace FileService.Database
{
    /// <summary>
    /// A class that defines the tables and its properties in the database for the FileService.
    /// </summary>
    public class FileServiceDbContext : DbContext
    {
        public FileServiceDbContext(DbContextOptions<FileServiceDbContext> options)
            :base(options)
        {
        }
        
        public DbSet<DbFile> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }
    }
}
