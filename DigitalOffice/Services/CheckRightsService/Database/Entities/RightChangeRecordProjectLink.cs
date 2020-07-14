using System;
using System.ComponentModel.DataAnnotations;

namespace CheckRightsService.Database.Entities
{
    public class RightChangeRecordProjectLink
    {
        [Required]
        public Guid RightChangeRecordId { get; set; }
        [Required]
        public RightChangeRecord RightChangeRecord { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
    }
}
