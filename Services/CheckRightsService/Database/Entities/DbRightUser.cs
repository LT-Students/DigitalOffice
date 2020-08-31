using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.CheckRightsService.Database.Entities
{
    public class DbRightUser
    {
        public int RightId { get; set; }
        public DbRight Right { get; set; }
        public Guid UserId { get; set; }
    }

    public class RightUserConfiguration : IEntityTypeConfiguration<DbRightUser>
    {
        public void Configure(EntityTypeBuilder<DbRightUser> builder)
        {
            builder.HasKey(rightUser => new { rightUser.RightId, rightUser.UserId });

            builder.HasOne(rightUser => rightUser.Right)
                .WithMany(right => right.UserIds)
                .HasForeignKey(rightUser => rightUser.RightId);
        }
    }
}