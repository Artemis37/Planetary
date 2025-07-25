﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Planetary.Infrastructure.Context;

#nullable disable

namespace Planetary.Infrastructure.Migrations
{
    [DbContext(typeof(PlanetaryContext))]
    [Migration("20250613174721_SeedingData")]
    partial class SeedingData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Planetary.Domain.Models.Criteria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<double>("MaximumThreshold")
                        .HasColumnType("float");

                    b.Property<double>("MinimumThreshold")
                        .HasColumnType("float");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Criteria");
                });

            modelBuilder.Entity("Planetary.Domain.Models.Planet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AtmosphericComposition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("AtmosphericPressure")
                        .HasColumnType("float");

                    b.Property<DateTime>("DiscoveryDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("DistanceFromEarth")
                        .HasColumnType("float");

                    b.Property<bool>("HasAtmosphere")
                        .HasColumnType("bit");

                    b.Property<bool>("HasWater")
                        .HasColumnType("bit");

                    b.Property<double>("Mass")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PlanetType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Radius")
                        .HasColumnType("float");

                    b.Property<string>("StellarSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SurfaceGravity")
                        .HasColumnType("float");

                    b.Property<double>("SurfaceTemperature")
                        .HasColumnType("float");

                    b.Property<double>("WaterCoverage")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("Planetary.Domain.Models.PlanetCriteria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CriteriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EvaluationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMet")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlanetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CriteriaId");

                    b.HasIndex("PlanetId");

                    b.ToTable("PlanetCriteria");
                });

            modelBuilder.Entity("Planetary.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FavoritePlanets")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredPlanetarySystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResearchInterests")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Planetary.Domain.Models.PlanetCriteria", b =>
                {
                    b.HasOne("Planetary.Domain.Models.Criteria", "Criteria")
                        .WithMany("PlanetCriteria")
                        .HasForeignKey("CriteriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Planetary.Domain.Models.Planet", "Planet")
                        .WithMany("PlanetCriteria")
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Criteria");

                    b.Navigation("Planet");
                });

            modelBuilder.Entity("Planetary.Domain.Models.Criteria", b =>
                {
                    b.Navigation("PlanetCriteria");
                });

            modelBuilder.Entity("Planetary.Domain.Models.Planet", b =>
                {
                    b.Navigation("PlanetCriteria");
                });
#pragma warning restore 612, 618
        }
    }
}
