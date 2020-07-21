using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class RightProjectLink
    {
        public Guid RightId { get; set; }
        public Right Right { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class RightProjectLinkConfiguration : IEntityTypeConfiguration<RightProjectLink>
    {
        public void Configure(EntityTypeBuilder<RightProjectLink> builder)
        {
            builder.HasKey(link => new { link.RightId, link.ProjectId });

            builder
                .HasOne(link => link.Right)
                .WithMany(r => r.PermissionsIds)
                .HasForeignKey(link => link.RightId);
        }
    }
}
