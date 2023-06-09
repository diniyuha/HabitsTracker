﻿// <auto-generated />
using System;
using HabitsTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HabitsTracker.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230421034106_AddRole")]
    partial class AddRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.16");

            modelBuilder.Entity("HabitsTracker.Data.Entities.FrequencyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("DayNumber")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("HabitId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.ToTable("Frequencies");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.HabitEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateForm")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Goal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GoalPeriod")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.HasIndex("UserId");

                    b.ToTable("Habits");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.HabitReminderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HabitId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeReminder")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.ToTable("HabitReminders");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.HabitsDictionaryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateForm")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Goal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GoalPeriod")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.ToTable("HabitsDictionary");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.UnitEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ColorTheme")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .HasColumnType("TEXT");

                    b.Property<int>("Language")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.FrequencyEntity", b =>
                {
                    b.HasOne("HabitsTracker.Data.Entities.HabitEntity", "Habit")
                        .WithMany("Frequencies")
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.HabitEntity", b =>
                {
                    b.HasOne("HabitsTracker.Data.Entities.UnitEntity", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HabitsTracker.Data.Entities.UserEntity", "User")
                        .WithMany("Habits")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.HabitReminderEntity", b =>
                {
                    b.HasOne("HabitsTracker.Data.Entities.HabitEntity", "Habit")
                        .WithMany("Reminders")
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.HabitsDictionaryEntity", b =>
                {
                    b.HasOne("HabitsTracker.Data.Entities.UnitEntity", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.HabitEntity", b =>
                {
                    b.Navigation("Frequencies");

                    b.Navigation("Reminders");
                });

            modelBuilder.Entity("HabitsTracker.Data.Entities.UserEntity", b =>
                {
                    b.Navigation("Habits");
                });
#pragma warning restore 612, 618
        }
    }
}
