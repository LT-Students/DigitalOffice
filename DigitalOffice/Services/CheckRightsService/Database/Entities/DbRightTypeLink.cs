using CheckRightsService.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CheckRightsService.Database.Entities
{
    public class DbRightTypeLink
    {
        public Guid RightId { get; set; }
        public DbRight Right { get; set; }
        public RightType RightType { get; set; }
    }

    public class RightTypeLinkConfiguration : IEntityTypeConfiguration<DbRightTypeLink>
    {
        public void Configure(EntityTypeBuilder<DbRightTypeLink> builder)
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
