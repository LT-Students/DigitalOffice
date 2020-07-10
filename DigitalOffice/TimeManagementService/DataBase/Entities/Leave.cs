using System;
using System.ComponentModel.DataAnnotations;

namespace TimeManagementService.DataBase.Entities
{
    public class Leave
    {
        [Key]
        public Guid Id { get; set; }
        public Guid WorkerId { get; set; }
        public DateTime StartLeave { get; set; }
        public DateTime EndLeave { get; set; }
        public TypeLeave TypeLeave { get; set; }        
        public string Comment { get; set; }
    }
}
