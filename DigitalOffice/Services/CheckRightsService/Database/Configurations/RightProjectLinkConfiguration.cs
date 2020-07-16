using CheckRightsService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckRightsService.Database.Configurations
{
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
