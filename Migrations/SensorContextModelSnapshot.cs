﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend_solar.Repositories;

#nullable disable

namespace backend_solar.Migrations
{
    [DbContext(typeof(SensorContext))]
    partial class SensorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PlaceUser", b =>
                {
                    b.Property<Guid>("AllowedUsersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserPlacesId")
                        .HasColumnType("uuid");

                    b.HasKey("AllowedUsersId", "UserPlacesId");

                    b.HasIndex("UserPlacesId");

                    b.ToTable("PlaceUser");
                });

            modelBuilder.Entity("backend_solar.Models.DirtSensorData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DirtLevel")
                        .HasColumnType("integer");

                    b.Property<bool>("IsNetworkWorking")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStatusOk")
                        .HasColumnType("boolean");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id", "Timestamp");

                    b.ToTable("DirtSensorData");
                });

            modelBuilder.Entity("backend_solar.Models.LuminositySensorData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsNetworkWorking")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStatusOk")
                        .HasColumnType("boolean");

                    b.Property<double>("Luminosity")
                        .HasColumnType("double precision");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id", "Timestamp");

                    b.ToTable("LuminositySensorData");
                });

            modelBuilder.Entity("backend_solar.Models.Place", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("backend_solar.Models.Sensor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlaceId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("backend_solar.Models.SolarPanelData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("CurrentPowerGeneration")
                        .HasColumnType("double precision");

                    b.Property<bool>("IsNetworkWorking")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStatusOk")
                        .HasColumnType("boolean");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id", "Timestamp");

                    b.ToTable("SolarPanelData");
                });

            modelBuilder.Entity("backend_solar.Models.TemperatureSensorData", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsNetworkWorking")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStatusOk")
                        .HasColumnType("boolean");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id", "Timestamp");

                    b.ToTable("TemperatureSensorData");
                });

            modelBuilder.Entity("backend_solar.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PlaceUser", b =>
                {
                    b.HasOne("backend_solar.Models.User", null)
                        .WithMany()
                        .HasForeignKey("AllowedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_solar.Models.Place", null)
                        .WithMany()
                        .HasForeignKey("UserPlacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend_solar.Models.Sensor", b =>
                {
                    b.HasOne("backend_solar.Models.Place", null)
                        .WithMany("Sensors")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend_solar.Models.Place", b =>
                {
                    b.Navigation("Sensors");
                });
#pragma warning restore 612, 618
        }
    }
}
