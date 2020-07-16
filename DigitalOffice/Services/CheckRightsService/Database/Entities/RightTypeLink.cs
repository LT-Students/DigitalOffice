using CheckRightsService.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class RightTypeLink
    {
        public Guid RightId { get; set; }
        public Right Right { get; set; }
        public RightType RightType { get; set; }
    }

    public class RightTypeLinkConfiguration : IEntityTypeConfiguration<RightTypeLink>
    {
        public void Configure(EntityTypeBuilder<RightTypeLink> builder)
        {
            builder.HasKey(link => new { link.RightId, link.RightType });

            builder
                .HasOne(link => link.Right)
                .WithMany(r => r.Types)
                .HasForeignKey(link => link.RightId);

            builder
            .Property(e => e.RightType)
            .HasConversion<int>();
        }
    }
}
