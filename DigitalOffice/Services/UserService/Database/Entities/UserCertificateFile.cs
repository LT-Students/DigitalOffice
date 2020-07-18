using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace UserService.Database.Entities
{
    public class UserCertificateFile
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid CertificateId { get; set; }
    }

    public class UserCertificateFileConfiguration : IEntityTypeConfiguration<UserCertificateFile>
    {
        public void Configure(EntityTypeBuilder<UserCertificateFile> builder)
        {
            builder.HasKey(pm => new { pm.UserId, pm.CertificateId });
            builder.HasOne<User>(pm => pm.User)
                .WithMany(p => p.CertificatesFilesIds)
                .HasForeignKey(pm => pm.UserId);
        }
    }
}
