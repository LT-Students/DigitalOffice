using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectService.Database.Entities
{
    public class ProjectFile
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid FileId { get; set; }
    }

    public class ProjectFileConfiguration : IEntityTypeConfiguration<ProjectFile>
    {
        public void Configure(EntityTypeBuilder<ProjectFile> builder)
        {
            builder.HasKey(projectFile => new {projectFile.ProjectId, projectFile.FileId});
        }
    }
}