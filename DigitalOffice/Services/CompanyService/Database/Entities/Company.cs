﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyService.Database.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
        
        public ICollection<CompanyUser> UserIds { get; set; }
    }
}