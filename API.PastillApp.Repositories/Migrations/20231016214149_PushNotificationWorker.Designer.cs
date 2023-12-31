﻿// <auto-generated />
using System;
using API.PastillApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.PastillApp.Repositories.Migrations
{
    [DbContext(typeof(PastillAppContext))]
    [Migration("20231016214149_PushNotificationWorker")]
    partial class PushNotificationWorker
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.PastillApp.Domain.Entities.AlertLog", b =>
                {
                    b.Property<int>("AlertLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlertLogId"));

                    b.Property<int?>("EmergencyUserId")
                        .HasColumnType("int");

                    b.Property<int>("ReminderLogId")
                        .HasColumnType("int");

                    b.HasKey("AlertLogId");

                    b.HasIndex("EmergencyUserId");

                    b.HasIndex("ReminderLogId");

                    b.ToTable("AlertLogs");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.DailyStatus", b =>
                {
                    b.Property<int>("DailyStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DailyStatusID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observation")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Symptoms")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DailyStatusID");

                    b.HasIndex("UserId");

                    b.ToTable("DailyStatuses");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.Medicine", b =>
                {
                    b.Property<int>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicineId"));

                    b.Property<int>("Dosage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicineId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.Reminder", b =>
                {
                    b.Property<int>("ReminderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReminderId"));

                    b.Property<DateTime>("DateTimeStart")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmergencyAlert")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FrequencyNumber")
                        .HasColumnType("int");

                    b.Property<string>("FrequencyText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IntakeTimeNumber")
                        .HasColumnType("int");

                    b.Property<string>("IntakeTimeText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.Property<string>("Observation")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Presentation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReminderId");

                    b.HasIndex("MedicineId");

                    b.HasIndex("UserId");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.ReminderLog", b =>
                {
                    b.Property<int>("ReminderLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReminderLogId"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Notificated")
                        .HasColumnType("bit");

                    b.Property<int>("ReminderId")
                        .HasColumnType("int");

                    b.Property<bool>("SecondNotification")
                        .HasColumnType("bit");

                    b.Property<bool>("Taken")
                        .HasColumnType("bit");

                    b.HasKey("ReminderLogId");

                    b.HasIndex("ReminderId");

                    b.ToTable("ReminderLogs");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.Token", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenId"));

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TokenId");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("EmergencyUserId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("EmergencyUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.AlertLog", b =>
                {
                    b.HasOne("API.PastillApp.Domain.Entities.User", "EmergencyUser")
                        .WithMany()
                        .HasForeignKey("EmergencyUserId");

                    b.HasOne("API.PastillApp.Domain.Entities.ReminderLog", "ReminderLog")
                        .WithMany()
                        .HasForeignKey("ReminderLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmergencyUser");

                    b.Navigation("ReminderLog");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.DailyStatus", b =>
                {
                    b.HasOne("API.PastillApp.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.Reminder", b =>
                {
                    b.HasOne("API.PastillApp.Domain.Entities.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.PastillApp.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.ReminderLog", b =>
                {
                    b.HasOne("API.PastillApp.Domain.Entities.Reminder", "Reminder")
                        .WithMany()
                        .HasForeignKey("ReminderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reminder");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.Token", b =>
                {
                    b.HasOne("API.PastillApp.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.PastillApp.Domain.Entities.User", b =>
                {
                    b.HasOne("API.PastillApp.Domain.Entities.User", "EmergencyUser")
                        .WithMany()
                        .HasForeignKey("EmergencyUserId");

                    b.Navigation("EmergencyUser");
                });
#pragma warning restore 612, 618
        }
    }
}
