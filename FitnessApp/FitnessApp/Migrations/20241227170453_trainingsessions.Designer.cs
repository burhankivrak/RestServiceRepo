﻿// <auto-generated />
using System;
using FitnessApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FitnessApp.Migrations
{
    [DbContext(typeof(FitnessContext))]
    [Migration("20241227170453_trainingsessions")]
    partial class trainingsessions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FitnessApp.Model.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("equipment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("device_type");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("equipment", "dbo");
                });

            modelBuilder.Entity("FitnessApp.Model.FitnessProgram", b =>
                {
                    b.Property<string>("ProgramCode")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("programCode");

                    b.Property<int>("MaxMembers")
                        .HasColumnType("int")
                        .HasColumnName("max_members");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("startdate");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("target");

                    b.HasKey("ProgramCode");

                    b.ToTable("program", "dbo");
                });

            modelBuilder.Entity("FitnessApp.Model.Members", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("member_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.Property<string>("Emailadres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<DateTime>("Geboortedatum")
                        .HasColumnType("datetime2")
                        .HasColumnName("birthday");

                    b.Property<string>("Interesses")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("interests");

                    b.Property<string>("TypeKlant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("membertype");

                    b.Property<string>("Verblijfsplaats")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.HasKey("Id");

                    b.ToTable("members", "dbo");
                });

            modelBuilder.Entity("FitnessApp.Model.ProgramMembers", b =>
                {
                    b.Property<string>("ProgramCode")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("programCode")
                        .HasColumnOrder(0);

                    b.Property<int>("MemberId")
                        .HasColumnType("int")
                        .HasColumnName("member_id")
                        .HasColumnOrder(1);

                    b.HasKey("ProgramCode", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("programmembers", "dbo");
                });

            modelBuilder.Entity("FitnessApp.Model.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("reservation_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("date");

                    b.Property<int>("MemberId")
                        .HasColumnType("int")
                        .HasColumnName("member_id");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("reservation", "dbo");
                });

            modelBuilder.Entity("FitnessApp.Model.ReservationTimeslot", b =>
                {
                    b.Property<int>("ReservationTimeslotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("reservation_time_slot_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationTimeslotId"));

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int")
                        .HasColumnName("equipment_id");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int")
                        .HasColumnName("reservation_id");

                    b.Property<int>("TimeslotId")
                        .HasColumnType("int")
                        .HasColumnName("time_slot_id");

                    b.HasKey("ReservationTimeslotId");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("ReservationId");

                    b.HasIndex("TimeslotId");

                    b.ToTable("ReservationTimeslot", "dbo");
                });

            modelBuilder.Entity("FitnessApp.Model.Timeslot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("time_slot_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EndTime")
                        .HasColumnType("int")
                        .HasColumnName("end_time");

                    b.Property<string>("PartOfDay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("part_of_day");

                    b.Property<int>("StartTime")
                        .HasColumnType("int")
                        .HasColumnName("start_time");

                    b.HasKey("Id");

                    b.ToTable("time_slot", "dbo");
                });

            modelBuilder.Entity("FitnessApp.Model.ProgramMembers", b =>
                {
                    b.HasOne("FitnessApp.Model.Members", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessApp.Model.FitnessProgram", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("FitnessApp.Model.Reservation", b =>
                {
                    b.HasOne("FitnessApp.Model.Members", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("FitnessApp.Model.ReservationTimeslot", b =>
                {
                    b.HasOne("FitnessApp.Model.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessApp.Model.Reservation", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessApp.Model.Timeslot", "Timeslot")
                        .WithMany()
                        .HasForeignKey("TimeslotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Reservation");

                    b.Navigation("Timeslot");
                });
#pragma warning restore 612, 618
        }
    }
}
