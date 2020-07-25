using System;
using System.ComponentModel.DataAnnotations;

namespace TimeManagementService.Database.Entities
{
    public class DbWorkTime
    {
        [Key]
        public Guid Id { get; set; }
        public Guid WorkerUserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Title { get; set; }
        public Guid ProjectId { get; set; }
        public string Comment { get; set; }
    }
}
