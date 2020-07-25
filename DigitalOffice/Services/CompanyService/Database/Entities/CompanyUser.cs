using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyService.Database.Entities
{
    public class CompanyUser
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public Guid PositionId { get; set; }
        public Position Position { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
    {
        public void Configure(EntityTypeBuilder<CompanyUser> builder)
        {
            builder.HasKey(user => new { user.UserId, user.CompanyId, user.PositionId });

            builder.HasOne(user => user.Company)
                .WithMany(company => company.UserIds)
                .HasForeignKey(user => user.CompanyId);

            builder.HasOne(user => user.Position)
                .WithMany(position => position.UserIds)
                .HasForeignKey(user => user.PositionId);
        }
    }
}