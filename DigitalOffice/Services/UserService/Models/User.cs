using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.UserService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public Guid? AvatarId { get; set; }
        public bool IsAdmin { get; set; }
        public List<Guid> CertificatesIds { get; set; }
        public List<Achievement> Achievements { get; set; }
     }
}