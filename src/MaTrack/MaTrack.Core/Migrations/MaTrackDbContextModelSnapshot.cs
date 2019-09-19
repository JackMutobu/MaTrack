﻿// <auto-generated />
using System;
using MaTrack.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MaTrack.Core.Migrations
{
    [DbContext(typeof(MaTrackDbContext))]
    partial class MaTrackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MaTrack.Core.Entities.AdminEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("DoB");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<string>("Phone");

                    b.Property<string>("ProfileLink");

                    b.Property<string>("SACCO");

                    b.Property<int>("StageId");

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("MaTrack.Core.Entities.DriverEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("DoB");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<string>("Phone");

                    b.Property<string>("ProfileLink");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UploadDate");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("MaTrack.Core.Entities.RouteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RouteName");

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("Id");

                    b.ToTable("RouteEntity");
                });

            modelBuilder.Entity("MaTrack.Core.Entities.RouteStageEntity", b =>
                {
                    b.Property<int>("RouteId");

                    b.Property<int>("StageId");

                    b.Property<int>("StagePosition");

                    b.HasKey("RouteId", "StageId");

                    b.HasIndex("StageId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("MaTrack.Core.Entities.StageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GPSCoordinate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("Id");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("MaTrack.Core.Entities.VehicleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Model");

                    b.Property<string>("Name");

                    b.Property<string>("NumPlate");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("MaTrack.Core.Entities.VehicleRouteEntity", b =>
                {
                    b.Property<int>("RouteId");

                    b.Property<int>("VeicleId");

                    b.HasKey("RouteId", "VeicleId");

                    b.HasIndex("VeicleId");

                    b.ToTable("VehicleRouteEntity");
                });

            modelBuilder.Entity("MaTrack.Core.Entities.AdminEntity", b =>
                {
                    b.HasOne("MaTrack.Core.Entities.StageEntity", "Stage")
                        .WithMany()
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaTrack.Core.Entities.DriverEntity", b =>
                {
                    b.HasOne("MaTrack.Core.Entities.VehicleEntity", "Vehicle")
                        .WithOne("Driver")
                        .HasForeignKey("MaTrack.Core.Entities.DriverEntity", "VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaTrack.Core.Entities.RouteStageEntity", b =>
                {
                    b.HasOne("MaTrack.Core.Entities.RouteEntity", "Route")
                        .WithMany("RouteStages")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MaTrack.Core.Entities.StageEntity", "Stage")
                        .WithMany("RouteStages")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaTrack.Core.Entities.VehicleRouteEntity", b =>
                {
                    b.HasOne("MaTrack.Core.Entities.RouteEntity", "Route")
                        .WithMany("VehicleRoutes")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MaTrack.Core.Entities.VehicleEntity", "Vehicle")
                        .WithMany("VehicleRoutes")
                        .HasForeignKey("VeicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
