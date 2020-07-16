﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FileService.Database.Entities
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentExtension { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
