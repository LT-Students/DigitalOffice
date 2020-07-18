using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid? CEOUserId { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public ICollection<Position> Positions { get; set; }
    }
}
