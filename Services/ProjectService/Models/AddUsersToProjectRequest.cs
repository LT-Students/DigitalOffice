using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ProjectService.Models
{
    public class ProjectUser
    {
        public bool IsManager { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class AddUserToProjectRequest
    {
        public IEnumerable<ProjectUser> UsersToAdd { get; set; }
    }
}