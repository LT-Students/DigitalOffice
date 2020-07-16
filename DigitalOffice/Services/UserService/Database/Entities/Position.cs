using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Entities
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
    }
}
