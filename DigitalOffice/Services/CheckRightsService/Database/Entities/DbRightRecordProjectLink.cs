using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class DbRightRecordProjectLink
    {
        public Guid RightChangeRecordId { get; set; }
        public DbRightChangeRecord RightChangeRecord { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class RightRecordProjectLinkConfiguration : IEntityTypeConfiguration<DbRightRecordProjectLink>
    {
        public void Configure(EntityTypeBuilder<DbRightRecordProjectLink> builder)
        {
            builder.HasKey(link => new { link.RightChangeRecordId, link.ProjectId });

            builder
                .HasOne(link => link.RightChangeRecord)
                .WithMany(r => r.ChangedPermissionsIds)
                .HasForeignKey(link => link.RightChangeRecordId);
        }
    }
}
