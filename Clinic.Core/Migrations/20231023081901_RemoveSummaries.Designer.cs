﻿// <auto-generated />
using System;
using Clinic.Core.Appointments.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clinic.Core.Migrations
{
    [DbContext(typeof(AppointmentsDbContext))]
    [Migration("20231023081901_RemoveSummaries")]
    partial class RemoveSummaries
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("Clinic.Core.Appointments.Domain.Patients.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("CardNumber");

                    b.Property<DateTimeOffset?>("DateOfBirth")
                        .HasColumnType("TEXT")
                        .HasColumnName("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("FullName");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Gender");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("patients", "Appointments");
                });

            modelBuilder.Entity("Clinic.Core.Appointments.Domain.Visits.Visit", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("TEXT")
                        .HasColumnName("Date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Description");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("TEXT")
                        .HasColumnName("PatientId");

                    b.Property<string>("Physician")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Physician");

                    b.Property<string>("Procedures")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Procedures");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("visits", "Appointments");
                });

            modelBuilder.Entity("Clinic.Core.Appointments.Domain.Visits.Visit", b =>
                {
                    b.HasOne("Clinic.Core.Appointments.Domain.Patients.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
