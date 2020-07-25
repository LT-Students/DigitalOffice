using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class DbRightProjectLink
    {
        public Guid RightId { get; set; }
        public DbRight Right { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class RightProjectLinkConfiguration : IEntityTypeConfiguration<DbRightProjectLink>
    {
        public void Configure(EntityTypeBuilder<DbRightProjectLink> builder)
        {
            builder.HasKey(link => new { link.RightId, link.ProjectId });

            builder
                .HasOne(link => link.Right)
                .WithMany(r => r.PermissionsIds)
                .HasForeignKey(link => link.RightId);
        }
    }
}
