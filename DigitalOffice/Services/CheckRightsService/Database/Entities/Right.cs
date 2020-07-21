using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckRightsService.Database.Entities
{
    public class Right
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public ICollection<RightTypeLink> Types { get; set; }
        public ICollection<RightProjectLink> PermissionsIds { get; set; }
    }
}
