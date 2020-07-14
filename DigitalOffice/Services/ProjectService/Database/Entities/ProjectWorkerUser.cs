using System;

namespace ProjectService.Database.Entities
{
    public class ProjectWorkerUser
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid WorkerUserId { get; set; }

    }
}