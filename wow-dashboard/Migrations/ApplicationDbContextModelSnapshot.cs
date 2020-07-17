﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using wow_dashboard.Data;

namespace wow_dashboard.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("wow_dashboard.Models.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayableRaceGameId")
                        .HasColumnType("int");

                    b.Property<string>("Realm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("wow_dashboard.Models.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTodaysGoal")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("RefreshFrequency")
                        .HasColumnType("int");

                    b.Property<int>("TaskType")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("wow_dashboard.Models.TaskCharacter", b =>
                {
                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CharacterId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskCharacters");
                });

            modelBuilder.Entity("wow_dashboard.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BlizzardId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultRealm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DefaultTaskType")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoogleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("wow_dashboard.Models.Character", b =>
                {
                    b.HasOne("wow_dashboard.Models.User", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("wow_dashboard.Models.PlayableClass", "Class", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsMany("wow_dashboard.Models.Profession", "Professions", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.HasKey("CharacterId", "Id");

                            b1.ToTable("Profession");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });
                });

            modelBuilder.Entity("wow_dashboard.Models.Task", b =>
                {
                    b.HasOne("wow_dashboard.Models.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("wow_dashboard.Models.CollectibleType", "CollectibleType", b1 =>
                        {
                            b1.Property<Guid>("TaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("TaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });

                    b.OwnsMany("wow_dashboard.Models.GameDataReference", "GameDataReferences", b1 =>
                        {
                            b1.Property<Guid>("TaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("TaskId", "Id");

                            b1.ToTable("GameDataReference");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });

                    b.OwnsOne("wow_dashboard.Models.Source", "Source", b1 =>
                        {
                            b1.Property<Guid>("TaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("TaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });

                    b.OwnsMany("wow_dashboard.Models.WowheadDataReference", "WowheadDataReferences", b1 =>
                        {
                            b1.Property<Guid>("TaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("TaskId", "Id");

                            b1.ToTable("WowheadDataReference");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });
                });

            modelBuilder.Entity("wow_dashboard.Models.TaskCharacter", b =>
                {
                    b.HasOne("wow_dashboard.Models.Character", "Character")
                        .WithMany("TaskCharacters")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("wow_dashboard.Models.Task", "Task")
                        .WithMany("TaskCharacters")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
