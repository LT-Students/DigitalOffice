using System;

namespace UserService.Database.Entities
{
    public class UserCertificateFile
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid CertificateId { get; set; }
    }
}
