using CheckRightsService.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class RightChangeRecordTypeLink
    {
        public Guid RightChangeRecordId { get; set; }
        public RightChangeRecord RightChangeRecord { get; set; }
        public RightType RightType { get; set; }
    }

    public class RightChangeRecordTypeLinkConfiguration : IEntityTypeConfiguration<RightChangeRecordTypeLink>
    {
        public void Configure(EntityTypeBuilder<RightChangeRecordTypeLink> builder)
        {
            builder.HasKey(link => new { link.RightChangeRecordId, link.RightType });

            builder
                .HasOne(link => link.RightChangeRecord)
                .WithMany(r => r.Types)
                .HasForeignKey(link => link.RightChangeRecordId);

            builder
                .Property(e => e.RightType)
                .HasConversion<int>();
        }
    }
}
