using System;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.FileService.Database.Entities
{
    public class DbFile
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentExtension { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
