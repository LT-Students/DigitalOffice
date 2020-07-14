using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckRightsService.Database.Entities
{
    public class RightChangeRecord
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RightId { get; set; }
        public Guid ChangedById { get; set; }
        public DateTime Time { get; set; }
    }
}
