﻿using System;

namespace LT.DigitalOffice.UserService.Models
{
    public class EditUserRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public Guid? AvatarFileId { get; set; }
    }
}
