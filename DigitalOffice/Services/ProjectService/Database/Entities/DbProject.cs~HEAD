using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ProjectService.Database.Entities
{
    public class DbProject
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid DepartmentId { get; set; }
        public bool Deferred { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DbProjectManagerUser> ManagersUsersIds { get; set; }
        public ICollection<DbProjectWorkerUser> WorkersUsersIds { get; set; }
        public ICollection<DbProjectFile> FilesIds { get; set; }
    }
}