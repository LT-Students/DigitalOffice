using System;

namespace ProjectService.Database.Entities
{
    public class ProjectManagerUser
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid ManagerUserId { get; set; }
    }
}