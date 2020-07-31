using System;

namespace ProjectService.Models
{
    public class AddUserToProjectRequest
    {
        public bool IsManager { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}