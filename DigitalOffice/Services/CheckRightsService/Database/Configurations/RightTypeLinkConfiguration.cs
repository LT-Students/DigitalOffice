using CheckRightsService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckRightsService.Database.Configurations
{
    public class RightTypeLinkConfiguration : IEntityTypeConfiguration<RightTypeLink>
    {
        public void Configure(EntityTypeBuilder<RightTypeLink> builder)
        {
            builder.HasKey(link => new { link.RightId, link.RightTypeId });

            builder
                .HasOne(link => link.Right)
                .WithMany(r => r.Types)
                .HasForeignKey(link => link.RightId);

            builder
                .HasOne(link => link.RightType)
                .WithMany(t => t.Rights)
                .HasForeignKey(link => link.RightTypeId);
        }
    }
}
