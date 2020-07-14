using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Entities
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
    }
}
