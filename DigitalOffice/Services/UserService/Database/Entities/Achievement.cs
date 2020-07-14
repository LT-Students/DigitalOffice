using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Entities
{
    public class Achievement
    {
        [Key]
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Guid PictureFileId { get; set; }
        public DateTime Time { get; set; }
    }
}
