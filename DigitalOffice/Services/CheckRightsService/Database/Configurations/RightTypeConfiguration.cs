using System;
using CheckRightsService.Database.Entities;
using CheckRightsService.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckRightsService.Database.Configurations
{
    public class RightTypeConfiguration : IEntityTypeConfiguration<DbRightType>
    {
        public void Configure(EntityTypeBuilder<DbRightType> builder)
        {
            builder
            .Property(e => e.Type)
            .HasConversion(
                v => v.ToString(),
                v => (RightType)Enum.Parse(typeof(RightType), v));
        }
    }
}
