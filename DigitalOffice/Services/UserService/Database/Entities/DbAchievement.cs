using System;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.UserService.Database.Entities
{
    public class DbAchievement
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public Guid PictureFileId { get; set; }
    }
}
