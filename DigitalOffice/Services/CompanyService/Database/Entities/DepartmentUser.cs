using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyService.Database.Entities
{
    public class DepartmentUser
    {
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class DepartmentUserConfiguration : IEntityTypeConfiguration<DepartmentUser>
    {
        public void Configure(EntityTypeBuilder<DepartmentUser> builder)
        {
            builder.HasKey(user => new {user.UserId, user.DepartmentId});

            builder.HasOne(user => user.Department)
                .WithMany(department => department.UserIds)
                .HasForeignKey(user => user.DepartmentId);
        }
    }
}