﻿// <auto-generated />
using System;
using LT.DigitalOffice.CheckRightsService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LT.DigitalOffice.CheckRightsService.Database.Migrations
{
    [DbContext(typeof(CheckRightsServiceDbContext))]
    [Migration("20200823191047_RenamedTableRightUsers")]
    partial class RenamedTableRightUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LT.DigitalOffice.CheckRightsService.Database.Entities.DbRight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rights");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Add/Edit/Remove user"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Add/Edit/Remove project"
                        });
                });

            modelBuilder.Entity("LT.DigitalOffice.CheckRightsService.Database.Entities.DbRightUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RightId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RightId");

                    b.HasIndex("RightId");

                    b.ToTable("RightsUsers");
                });

            modelBuilder.Entity("LT.DigitalOffice.CheckRightsService.Database.Entities.DbRightUser", b =>
                {
                    b.HasOne("LT.DigitalOffice.CheckRightsService.Database.Entities.DbRight", "Right")
                        .WithMany("UserIds")
                        .HasForeignKey("RightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
