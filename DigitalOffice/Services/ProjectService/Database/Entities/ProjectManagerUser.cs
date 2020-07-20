using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectService.Database.Entities
{
    public class ProjectManagerUser
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid ManagerUserId { get; set; }
    }
    
    public class ProjectManagerUserConfiguration : IEntityTypeConfiguration<ProjectManagerUser>
    {
        public void Configure(EntityTypeBuilder<ProjectManagerUser> builder)
        {
            builder.HasKey(pm => new { pm.ProjectId, pm.ManagerUserId });
            
            builder.HasOne(pm => pm.Project)
                .WithMany(p => p.ManagersUsersIds)
                .HasForeignKey(pm => pm.ProjectId);  
        }
    }
}