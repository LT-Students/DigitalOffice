using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckRightsService.Database.Entities
{
    public class DbRight
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public ICollection<DbRightTypeLink> Types { get; set; }
        public ICollection<DbRightProjectLink> PermissionsIds { get; set; }
    }
}
