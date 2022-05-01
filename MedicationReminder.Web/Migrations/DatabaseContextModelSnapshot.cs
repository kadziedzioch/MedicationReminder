﻿// <auto-generated />
using System;
using MedicationReminder.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MedicationReminder.Web.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MedicationReminder.Web.Entities.MedicineEntity", b =>
                {
                    b.Property<string>("MedicineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsImmunosuppressive")
                        .HasColumnType("bit");

                    b.Property<string>("MedicineName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicineId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("MedicationReminder.Web.Entities.RemindTimeEntity", b =>
                {
                    b.Property<string>("RemindTimeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Dose")
                        .HasColumnType("int");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<string>("MedicineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RemindTimeId");

                    b.HasIndex("MedicineId");

                    b.HasIndex("UserId");

                    b.ToTable("RemindTimes");
                });

            modelBuilder.Entity("MedicationReminder.Web.Entities.UserEntity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MedicationReminder.Web.Entities.RemindTimeEntity", b =>
                {
                    b.HasOne("MedicationReminder.Web.Entities.MedicineEntity", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId");

                    b.HasOne("MedicationReminder.Web.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Medicine");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}