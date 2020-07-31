using System;
using System.Collections.Generic;

namespace UserService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Status { get; set; }
        public string PasswordHash { get; set; }
        public Guid? AvatarFileId { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<Guid> CertificatesFilesIds { get; set; }
        public IEnumerable<Guid> AchievementsIds { get; set; }
    }
}
