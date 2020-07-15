using Microsoft.EntityFrameworkCore;
using ProjectService.Database.Entities;

namespace ProjectService.Database
{
    /// <summary>
    /// A class that defines the tables and its properties in the database.
    /// For this particular case, it defines the database for the ProjectService.
    /// </summary>
    public class ProjectServiceDbContext : DbContext
    {
        public ProjectServiceDbContext (DbContextOptions<ProjectServiceDbContext> options)
            :base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        // Fluent API is written here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectManagerUser>(entity =>
            {
                entity.HasKey(pm => new { pm.ProjectId, pm.ManagerUserId });

                entity.HasOne<Project>(pm => pm.Project)
                    .WithMany(p => p.ManagersUsersIds)
                    .HasForeignKey(pm => pm.ProjectId);                
            });

            modelBuilder.Entity<ProjectWorkerUser>(entity =>
            {
                entity.HasKey(pw => new { pw.ProjectId, pw.WorkerUserId });

                entity.HasOne<Project>(pw => pw.Project)
                    .WithMany(p => p.WorkersUsersIds)
                    .HasForeignKey(pw => pw.ProjectId);
            });

            modelBuilder.Entity<ProjectFile>().HasKey(userFile => new { userFile.ProjectId, userFile.FileId });
        }
    }
}