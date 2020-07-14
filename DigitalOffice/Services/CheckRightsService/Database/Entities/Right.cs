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
        public Guid UserId { get; set; }
        public int Type { get; set; }
        [NotMapped]
        public ICollection<Guid> PermissionsIds { get; set; }
    }
}
