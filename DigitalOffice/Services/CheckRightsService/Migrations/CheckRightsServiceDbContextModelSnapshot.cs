﻿// <auto-generated />
using System;
using CheckRightsService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CheckRightsService.Migrations
{
    [DbContext(typeof(CheckRightsServiceDbContext))]
    partial class CheckRightsServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CheckRightsService.Database.Entities.DbRightType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RightTypes");
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.Right", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Rights");
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightChangeRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChangedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RightId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("RightsHistory");
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightChangeRecordTypeLink", b =>
                {
                    b.Property<Guid>("RightChangeRecordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RightTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RightChangeRecordId", "RightTypeId");

                    b.HasIndex("RightTypeId");

                    b.ToTable("RightChangeRecordTypeLink");
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightProjectLink", b =>
                {
                    b.Property<Guid>("RightId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RightId", "ProjectId");

                    b.ToTable("RightProjectLink");
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightRecordProjectLink", b =>
                {
                    b.Property<Guid>("RightChangeRecordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RightChangeRecordId", "ProjectId");

                    b.ToTable("RightRecordProjectLink");
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightTypeLink", b =>
                {
                    b.Property<Guid>("RightId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RightTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RightId", "RightTypeId");

                    b.HasIndex("RightTypeId");

                    b.ToTable("RightTypeLink");
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightChangeRecordTypeLink", b =>
                {
                    b.HasOne("CheckRightsService.Database.Entities.RightChangeRecord", "RightChangeRecord")
                        .WithMany("Types")
                        .HasForeignKey("RightChangeRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CheckRightsService.Database.Entities.DbRightType", "RightType")
                        .WithMany("RightChangeRecords")
                        .HasForeignKey("RightTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightProjectLink", b =>
                {
                    b.HasOne("CheckRightsService.Database.Entities.Right", "Right")
                        .WithMany("PermissionsIds")
                        .HasForeignKey("RightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightRecordProjectLink", b =>
                {
                    b.HasOne("CheckRightsService.Database.Entities.RightChangeRecord", "RightChangeRecord")
                        .WithMany("ChangedPermissionsIds")
                        .HasForeignKey("RightChangeRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckRightsService.Database.Entities.RightTypeLink", b =>
                {
                    b.HasOne("CheckRightsService.Database.Entities.Right", "Right")
                        .WithMany("Types")
                        .HasForeignKey("RightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CheckRightsService.Database.Entities.DbRightType", "RightType")
                        .WithMany("Rights")
                        .HasForeignKey("RightTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
