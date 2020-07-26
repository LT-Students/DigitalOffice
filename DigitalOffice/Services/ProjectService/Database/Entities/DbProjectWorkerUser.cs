using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectService.Database.Entities
{
    public class DbProjectWorkerUser
    {
        public Guid ProjectId { get; set; }
        public DbProject Project { get; set; }
        public Guid WorkerUserId { get; set; }
    }
    
    public class ProjectWorkerUserConfiguration : IEntityTypeConfiguration<DbProjectWorkerUser>
    {
        public void Configure(EntityTypeBuilder<DbProjectWorkerUser> builder)
        {
            builder.HasKey(pw => new { pw.ProjectId, pw.WorkerUserId });
            
            builder.HasOne(pw => pw.Project)
                .WithMany(p => p.WorkersUsersIds)
                .HasForeignKey(pw => pw.ProjectId);
        }
    }
}