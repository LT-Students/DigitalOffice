using CheckRightsService.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckRightsService.Database.Entities
{
    public class DbRightType
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public RightType Type { get; set; }
        public ICollection<RightTypeLink> Rights { get; set; }
        public ICollection<RightChangeRecordTypeLink> RightChangeRecords { get; set; }
    }

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
