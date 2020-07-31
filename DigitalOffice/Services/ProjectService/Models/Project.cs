using System;
using System.Collections.Generic;

namespace ProjectService.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid DepartmentId { get; set; }
        public bool Deferred { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Guid> ManagersUsersIds { get; set; }
        public ICollection<Guid> WorkersUsersIds { get; set; }
        public ICollection<Guid> FilesIds { get; set; }
    }
}