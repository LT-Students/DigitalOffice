using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckRightsService.Database.Entities
{
    public class RightChangeRecord
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid RightId { get; set; }
        [Required]
        public Guid ChangedById { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [NotMapped]
        public ICollection<RightChangeRecordProjectLink> ChangedPermissionsIds { get; set; }
    }
}
