using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectService.Database.Entities
{
    public class ProjectWorkerUser
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid WorkerUserId { get; set; }
    }
    
    public class ProjectWorkerUserConfiguration : IEntityTypeConfiguration<ProjectWorkerUser>
    {
        public void Configure(EntityTypeBuilder<ProjectWorkerUser> builder)
        {
            builder.HasKey(pw => new { pw.ProjectId, pw.WorkerUserId });
            
            builder.HasOne(pw => pw.Project)
                .WithMany(p => p.WorkersUsersIds)
                .HasForeignKey(pw => pw.ProjectId);
        }
    }
}