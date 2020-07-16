using CheckRightsService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckRightsService.Database.Configurations
{
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
