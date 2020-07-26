using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectService.Database.Entities
{
    public class DbProjectManagerUser
    {
        public Guid ProjectId { get; set; }
        public DbProject Project { get; set; }
        public Guid ManagerUserId { get; set; }
    }
    
    public class ProjectManagerUserConfiguration : IEntityTypeConfiguration<DbProjectManagerUser>
    {
        public void Configure(EntityTypeBuilder<DbProjectManagerUser> builder)
        {
            builder.HasKey(pm => new { pm.ProjectId, pm.ManagerUserId });
            
            builder.HasOne(pm => pm.Project)
                .WithMany(p => p.ManagersUsersIds)
                .HasForeignKey(pm => pm.ProjectId);  
        }
    }
}