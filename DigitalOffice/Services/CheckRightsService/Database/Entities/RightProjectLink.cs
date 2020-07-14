using System;
using System.ComponentModel.DataAnnotations;

namespace CheckRightsService.Database.Entities
{
    public class RightProjectLink
    {
        [Required]
        public Guid RightId { get; set; }
        [Required]
        public Right Right { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
    }
}
