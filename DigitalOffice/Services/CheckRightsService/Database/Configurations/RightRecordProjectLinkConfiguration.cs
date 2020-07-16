using CheckRightsService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckRightsService.Database.Configurations
{
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
