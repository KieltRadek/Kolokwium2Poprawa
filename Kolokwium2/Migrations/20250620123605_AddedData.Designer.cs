﻿// <auto-generated />
using System;
using Kolokwium2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kolokwium2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250620123605_AddedData")]
    partial class AddedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kolokwium2.Models.Race", b =>
                {
                    b.Property<int>("RaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RaceId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RaceId");

                    b.ToTable("Race");

                    b.HasData(
                        new
                        {
                            RaceId = 1,
                            Date = new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Silverstone, UK",
                            Name = "British Grand Prix"
                        },
                        new
                        {
                            RaceId = 2,
                            Date = new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Monte Carlo, Monaco",
                            Name = "Monaco Grand Prix"
                        });
                });

            modelBuilder.Entity("Kolokwium2.Models.RaceParticipation", b =>
                {
                    b.Property<int>("RacerId")
                        .HasColumnType("int");

                    b.Property<int>("TrackRaceId")
                        .HasColumnType("int");

                    b.Property<int>("FinishTimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("RacerId", "TrackRaceId");

                    b.HasIndex("TrackRaceId");

                    b.ToTable("Race_Participation");

                    b.HasData(
                        new
                        {
                            RacerId = 1,
                            TrackRaceId = 1,
                            FinishTimeInSeconds = 5460,
                            Position = 1
                        },
                        new
                        {
                            RacerId = 1,
                            TrackRaceId = 2,
                            FinishTimeInSeconds = 6300,
                            Position = 2
                        });
                });

            modelBuilder.Entity("Kolokwium2.Models.Racer", b =>
                {
                    b.Property<int>("RacerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RacerId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RacerId");

                    b.ToTable("Racer");

                    b.HasData(
                        new
                        {
                            RacerId = 1,
                            FirstName = "Lewis",
                            LastName = "Hamilton"
                        });
                });

            modelBuilder.Entity("Kolokwium2.Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackId"));

                    b.Property<decimal>("LengthInKm")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TrackId");

                    b.ToTable("Track");

                    b.HasData(
                        new
                        {
                            TrackId = 1,
                            LengthInKm = 5.89m,
                            Name = "Silverstone Circuit"
                        },
                        new
                        {
                            TrackId = 2,
                            LengthInKm = 3.34m,
                            Name = "Monaco Circuit"
                        });
                });

            modelBuilder.Entity("Kolokwium2.Models.TrackRace", b =>
                {
                    b.Property<int>("TrackRaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackRaceId"));

                    b.Property<int?>("BestTimeInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("Laps")
                        .HasColumnType("int");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("TrackRaceId");

                    b.HasIndex("RaceId");

                    b.HasIndex("TrackId");

                    b.ToTable("Track_Race");

                    b.HasData(
                        new
                        {
                            TrackRaceId = 1,
                            Laps = 52,
                            RaceId = 1,
                            TrackId = 1
                        },
                        new
                        {
                            TrackRaceId = 2,
                            Laps = 78,
                            RaceId = 2,
                            TrackId = 2
                        });
                });

            modelBuilder.Entity("Kolokwium2.Models.RaceParticipation", b =>
                {
                    b.HasOne("Kolokwium2.Models.Racer", "Racer")
                        .WithMany("RaceParticipations")
                        .HasForeignKey("RacerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kolokwium2.Models.TrackRace", "TrackRace")
                        .WithMany("RaceParticipations")
                        .HasForeignKey("TrackRaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Racer");

                    b.Navigation("TrackRace");
                });

            modelBuilder.Entity("Kolokwium2.Models.TrackRace", b =>
                {
                    b.HasOne("Kolokwium2.Models.Race", "Race")
                        .WithMany("TrackRaces")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kolokwium2.Models.Track", "Track")
                        .WithMany("TrackRaces")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Race");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Kolokwium2.Models.Race", b =>
                {
                    b.Navigation("TrackRaces");
                });

            modelBuilder.Entity("Kolokwium2.Models.Racer", b =>
                {
                    b.Navigation("RaceParticipations");
                });

            modelBuilder.Entity("Kolokwium2.Models.Track", b =>
                {
                    b.Navigation("TrackRaces");
                });

            modelBuilder.Entity("Kolokwium2.Models.TrackRace", b =>
                {
                    b.Navigation("RaceParticipations");
                });
#pragma warning restore 612, 618
        }
    }
}
