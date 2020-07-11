using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CEO { get; set; }
    }
}
