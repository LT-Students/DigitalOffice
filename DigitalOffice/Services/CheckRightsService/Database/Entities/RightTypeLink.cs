using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class RightTypeLink
    {
        public Guid RightId { get; set; }
        public Right Right { get; set; }
        public Guid RightTypeId { get; set; }
        public DbRightType RightType { get; set; }
    }

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
