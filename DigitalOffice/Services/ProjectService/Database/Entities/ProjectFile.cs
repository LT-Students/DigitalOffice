using System;

namespace ProjectService.Database.Entities
{
    public class ProjectFile
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid FileId { get; set; }
    }
}