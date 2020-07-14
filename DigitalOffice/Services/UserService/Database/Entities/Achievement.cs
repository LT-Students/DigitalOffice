using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Database.Entities
{
    public class Achievement
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public Guid PictureFileId { get; set; }
    }
}
