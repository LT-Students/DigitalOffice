﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.ProjectService.Database.Migrations
{
    [DbContext(typeof(ProjectServiceDbContext))]
    [Migration("20200714153124_ProjectFiles")]
    partial class ProjectFiles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectService.Database.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deferred")
                        .HasColumnType("bit");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Obsolete")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectService.Database.Entities.ProjectFile", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "FileId");

                    b.ToTable("ProjectFile");
                });

            modelBuilder.Entity("ProjectService.Database.Entities.ProjectManagerUser", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ManagerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "ManagerUserId");

                    b.ToTable("ProjectManagerUser");
                });

            modelBuilder.Entity("ProjectService.Database.Entities.ProjectWorkerUser", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProjectId", "WorkerUserId");

                    b.ToTable("ProjectWorkerUser");
                });

            modelBuilder.Entity("ProjectService.Database.Entities.ProjectFile", b =>
                {
                    b.HasOne("ProjectService.Database.Entities.Project", "Project")
                        .WithMany("FilesIds")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectService.Database.Entities.ProjectManagerUser", b =>
                {
                    b.HasOne("ProjectService.Database.Entities.Project", "Project")
                        .WithMany("ManagersUsersIds")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectService.Database.Entities.ProjectWorkerUser", b =>
                {
                    b.HasOne("ProjectService.Database.Entities.Project", "Project")
                        .WithMany("WorkersUsersIds")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
