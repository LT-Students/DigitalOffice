using System;
using System.ComponentModel.DataAnnotations;

namespace TimeManagementService.DataBase.Entities
{
    public class WorkTime
    {
        [Key]
        public Guid Id { get; set; }
        public Guid WorkerId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Title { get; set; }
        public Guid Project { get; set; }
        public string Comment { get; set; }
    }
}
