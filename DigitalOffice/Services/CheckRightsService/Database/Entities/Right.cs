using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckRightsService.Database.Entities
{
    public class Right
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int Type { get; set; }
        [NotMapped]
        public ICollection<RightProjectLink> PermissionsIds { get; set; }
    }
}
