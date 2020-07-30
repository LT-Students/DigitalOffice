﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectService.Database.Entities
{
    public class DbProjectFile
    {
        public Guid ProjectId { get; set; }
        public DbProject Project { get; set; }
        public Guid FileId { get; set; }
    }

    public class ProjectFileConfiguration : IEntityTypeConfiguration<DbProjectFile>
    {
        public void Configure(EntityTypeBuilder<DbProjectFile> builder)
        {
            builder.HasKey(projectFile => new {projectFile.ProjectId, projectFile.FileId});
        }
    }
}