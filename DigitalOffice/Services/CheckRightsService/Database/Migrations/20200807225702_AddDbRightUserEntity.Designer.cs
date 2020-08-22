﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.CheckRightsService.Database.Migrations
{
    [DbContext(typeof(CheckRightsServiceDbContext))]
    [Migration("20200807225702_AddDbRightUserEntity")]
    partial class AddDbRightUserEntity
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
                    b.Property<int>("RightId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RightId", "UserId");

                    b.ToTable("DbRightUser");
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