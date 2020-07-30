using Microsoft.EntityFrameworkCore;
using LT.DigitalOffice.FileService.Database.Entities;

namespace LT.DigitalOffice.FileService.Database
{
    /// <summary>
    /// A class that defines the tables and its properties in the database for the LT.DigitalOffice.FileService.
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
