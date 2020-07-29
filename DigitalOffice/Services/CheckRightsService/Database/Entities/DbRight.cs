using System;
using System.ComponentModel.DataAnnotations;

namespace CheckRightsService.Database.Entities
{
    public class DbRight
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
