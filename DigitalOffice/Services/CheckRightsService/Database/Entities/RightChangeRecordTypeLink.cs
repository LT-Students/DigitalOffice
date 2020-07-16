using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class RightChangeRecordTypeLink
    {
        public Guid RightChangeRecordId { get; set; }
        public RightChangeRecord RightChangeRecord { get; set; }
        public Guid RightTypeId { get; set; }
        public DbRightType RightType { get; set; }
    }

    public class RightChangeRecordTypeLinkConfiguration : IEntityTypeConfiguration<RightChangeRecordTypeLink>
    {
        public void Configure(EntityTypeBuilder<RightChangeRecordTypeLink> builder)
        {
            builder.HasKey(link => new { link.RightChangeRecordId, link.RightTypeId });

            builder
                .HasOne(link => link.RightChangeRecord)
                .WithMany(r => r.Types)
                .HasForeignKey(link => link.RightChangeRecordId);

            builder
                .HasOne(link => link.RightType)
                .WithMany(t => t.RightChangeRecords)
                .HasForeignKey(link => link.RightTypeId);
        }
    }
}
