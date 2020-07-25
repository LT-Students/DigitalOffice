using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckRightsService.Database.Entities
{
    public class DbRightChangeRecord
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid RightId { get; set; }
        [Required]
        public Guid ChangedByUserId { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public ICollection<DbRightChangeRecordTypeLink> Types { get; set; }
        public ICollection<DbRightRecordProjectLink> ChangedPermissionsIds { get; set; }
    }
}
