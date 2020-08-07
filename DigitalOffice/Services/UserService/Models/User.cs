using System;
using System.Collections.Generic;

<<<<<<< HEAD
namespace UserService.Models
=======
namespace LT.DigitalOffice.UserService.Models
>>>>>>> develop
{
    public class User
    {
        public Guid Id { get; set; }
<<<<<<< HEAD
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
=======
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public Guid? AvatarId { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<Guid> CertificatesIds { get; set; }
        public IEnumerable<Achievement> Achievements { get; set; }
     }
}
>>>>>>> develop
