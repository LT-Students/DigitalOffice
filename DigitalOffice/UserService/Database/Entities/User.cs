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
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Status { get; set; }
        public string PasswordHash { get; set; }
        public Guid AvatarId { get; set; }
        [NotMapped]
        public ICollection<Guid> CertificatesIds { get; set; }
        [NotMapped]
        public ICollection<Guid> PositionsIds { get; set; }
        [NotMapped]
        public ICollection<Guid> AchievementsIds { get; set; }
        public bool Obsolete { get; set; }
    }
}