using CheckRightsService.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckRightsService.Database.Entities
{
    public class DbRightType
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public RightType Type { get; set; }
        public ICollection<RightTypeLink> Rights { get; set; }
        public ICollection<RightChangeRecordTypeLink> RightChangeRecords { get; set; }
    }
}
