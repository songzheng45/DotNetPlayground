﻿// <auto-generated />
using System;
using EFCoreSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreSample.Migrations.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20181005115839_AddAuthorDescColumn")]
    partial class AddAuthorDescColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview2-35157")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EFCoreSample.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Desc");

                    b.Property<string>("ProfilePhoto");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            ProfilePhoto = "",
                            UserName = "Joe"
                        });
                });

            modelBuilder.Entity("EFCoreSample.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Blogs");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            AuthorId = 100,
                            PostDate = new DateTime(2018, 10, 5, 19, 58, 39, 194, DateTimeKind.Local).AddTicks(5771),
                            Title = "HOw to drive care",
                            UpdateTime = new DateTime(2018, 10, 5, 19, 58, 39, 195, DateTimeKind.Local).AddTicks(130)
                        },
                        new
                        {
                            Id = 101,
                            AuthorId = 100,
                            PostDate = new DateTime(2018, 10, 5, 19, 58, 39, 195, DateTimeKind.Local).AddTicks(650),
                            Title = "Let's go to Thailand",
                            UpdateTime = new DateTime(2018, 10, 5, 19, 58, 39, 195, DateTimeKind.Local).AddTicks(656)
                        });
                });

            modelBuilder.Entity("EFCoreSample.Models.Blog", b =>
                {
                    b.HasOne("EFCoreSample.Models.Author", "Author")
                        .WithMany("Blogs")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
