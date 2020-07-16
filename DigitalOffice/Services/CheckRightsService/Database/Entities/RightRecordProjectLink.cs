using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class RightRecordProjectLink
    {
        public Guid RightChangeRecordId { get; set; }
        public RightChangeRecord RightChangeRecord { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class RightRecordProjectLinkConfiguration : IEntityTypeConfiguration<RightRecordProjectLink>
    {
        public void Configure(EntityTypeBuilder<RightRecordProjectLink> builder)
        {
            builder.HasKey(link => new { link.RightChangeRecordId, link.ProjectId });

            builder
                .HasOne(link => link.RightChangeRecord)
                .WithMany(r => r.ChangedPermissionsIds)
                .HasForeignKey(link => link.RightChangeRecordId);
        }
    }
}
