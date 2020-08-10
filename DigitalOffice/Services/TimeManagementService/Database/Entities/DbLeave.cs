using LT.DigitalOffice.TimeManagementService.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.TimeManagementService.Database.Entities
{
    public class DbLeave
    {
        [Key]
        public Guid Id { get; set; }
        public Guid WorkerUserId { get; set; }
        public DateTime StartLeave { get; set; }
        public DateTime EndLeave { get; set; }
        public LeaveType LeaveType { get; set; }
        public string Comment { get; set; }
    }
}
