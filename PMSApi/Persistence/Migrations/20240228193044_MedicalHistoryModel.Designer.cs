﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240228193044_MedicalHistoryModel")]
    partial class MedicalHistoryModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Appointment", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("AppointmentStatus")
                        .HasColumnType("integer");

                    b.Property<int>("AppointmentType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("boolean");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("AppointmentId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Domain.MedicalHistory", b =>
                {
                    b.Property<Guid>("MedicalHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Allergics")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("text");

                    b.Property<string>("FamilyMedicalHistory")
                        .HasColumnType("text");

                    b.Property<decimal>("Height")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("MedicalProblems")
                        .HasColumnType("text");

                    b.Property<string>("MentalHealthProblems")
                        .HasColumnType("text");

                    b.Property<string>("SugreriesHistory")
                        .HasColumnType("text");

                    b.Property<string>("TestsPerformed")
                        .HasColumnType("text");

                    b.Property<string>("TreatmenPlans")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Vaccines")
                        .HasColumnType("text");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("MedicalHistoryId");

                    b.ToTable("MedicalHistories");
                });
#pragma warning restore 612, 618
        }
    }
}