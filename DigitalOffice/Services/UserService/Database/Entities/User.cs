using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Database.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Status { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public Guid? AvatarFileId { get; set; }
        [NotMapped]
        public ICollection<UserCertificateFile> CertificatesFilesIds { get; set; }
        [NotMapped]
        public ICollection<UserPosition> PositionsIds { get; set; }
        [NotMapped]
        public ICollection<UserAchievement> AchievementsIds { get; set; }
        public bool IsActive { get; set; }
    }
}