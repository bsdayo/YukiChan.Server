﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YukiChan.Server.Databases;

#nullable disable

namespace YukiChan.Server.Migrations.ArcaeaDb
{
    [DbContext(typeof(ArcaeaDbContext))]
    [Migration("20230202160435_InitArcaeaDb")]
    partial class InitArcaeaDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("YukiChan.Server.Models.Arcaea.ArcaeaAliasSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("alias");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("platform");

                    b.Property<string>("SongId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("song_id");

                    b.Property<DateTime>("SubmitTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("submit_time");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("arcaea_alias_submissions");
                });

            modelBuilder.Entity("YukiChan.Server.Models.Arcaea.ArcaeaDatabaseUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("ArcaeaId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("arcaea_id");

                    b.Property<string>("ArcaeaName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("arcaea_name");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("platform");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("arcaea_users");
                });

            modelBuilder.Entity("YukiChan.Shared.Models.Arcaea.ArcaeaUserPreferences", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<bool>("Best30ShowGrade")
                        .HasColumnType("INTEGER")
                        .HasColumnName("b30_show_grade");

                    b.Property<bool>("Dark")
                        .HasColumnType("INTEGER")
                        .HasColumnName("dark");

                    b.Property<bool>("Nya")
                        .HasColumnType("INTEGER")
                        .HasColumnName("nya");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("platform");

                    b.Property<bool>("SingleDynamicBackground")
                        .HasColumnType("INTEGER")
                        .HasColumnName("single_dynamic_bg");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("arcaea_preferences");
                });
#pragma warning restore 612, 618
        }
    }
}
