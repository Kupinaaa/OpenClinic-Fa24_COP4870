﻿// <auto-generated />
using System;
using Api.HospitalSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.HospitalSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241208175338_BillRename")]
    partial class BillRename
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api.HospitalSystem.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("PhysicianId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("PhysicianId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.AppointmentTreatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("TreatmentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("TreatmentId");

                    b.ToTable("AppointmentTreatments");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<double>("OutOfPocket")
                        .HasColumnType("float");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique();

                    b.HasIndex("PatientId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.InsurancePlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("CoinsurancePercent")
                        .HasColumnType("float");

                    b.Property<double>("Copay")
                        .HasColumnType("float");

                    b.Property<double>("Deductable")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("OOPM")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("InsurancePlans");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("InsurancePlanId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPayThisYear")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("InsurancePlanId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Physician", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("GraduationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LisenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specializations")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Physicians");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Appointment", b =>
                {
                    b.HasOne("Api.HospitalSystem.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.HospitalSystem.Models.Physician", "Physician")
                        .WithMany("Appointments")
                        .HasForeignKey("PhysicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Physician");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.AppointmentTreatment", b =>
                {
                    b.HasOne("Api.HospitalSystem.Models.Appointment", "Appointment")
                        .WithMany("AppointmentTreatments")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.HospitalSystem.Models.Treatment", "Treatment")
                        .WithMany()
                        .HasForeignKey("TreatmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Treatment");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Bill", b =>
                {
                    b.HasOne("Api.HospitalSystem.Models.Appointment", "AppointmentNav")
                        .WithOne("Bill")
                        .HasForeignKey("Api.HospitalSystem.Models.Bill", "AppointmentId");

                    b.HasOne("Api.HospitalSystem.Models.Patient", null)
                        .WithMany("Payments")
                        .HasForeignKey("PatientId");

                    b.Navigation("AppointmentNav");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Patient", b =>
                {
                    b.HasOne("Api.HospitalSystem.Models.InsurancePlan", "InsurancePlan")
                        .WithMany()
                        .HasForeignKey("InsurancePlanId");

                    b.Navigation("InsurancePlan");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Appointment", b =>
                {
                    b.Navigation("AppointmentTreatments");

                    b.Navigation("Bill");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Api.HospitalSystem.Models.Physician", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
