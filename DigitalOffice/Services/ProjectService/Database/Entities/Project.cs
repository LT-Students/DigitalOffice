using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectService.Database.Entities
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }     
        public Guid DepartmentId { get; set; }
        public bool Deferred { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ProjectManagerUser> ManagersUsersIds { get; set; }
        public ICollection<ProjectWorkerUser> WorkersUsersIds { get; set; }
        public ICollection<ProjectFile> FilesIds { get; set; }
    }
}