using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyService.Database.Entities
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CompanyUser> UserIds { get; set; }
    }
}