using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.CheckRightsService.Database.Entities
{
    public class DbRight
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DbRightUser> UserIds { get; set; }
    }

    public class DbRightConfiguration : IEntityTypeConfiguration<DbRight>
    {
        public void Configure(EntityTypeBuilder<DbRight> builder)
        {
            builder.HasData(
                new DbRight
                {
                    Id = 1,
                    Name = "Add/Edit/Remove user",
                    Description = null
                },
                new DbRight
                {
                    Id = 2,
                    Name = "Add/Edit/Remove project",
                    Description = null
                }
            );
        }
    }
}