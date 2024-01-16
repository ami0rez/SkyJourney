﻿// <auto-generated />
using System;
using Amirez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SkyJourney.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.15");

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.CityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_cities");

                    b.ToTable("cities", (string)null);
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.CustomerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<Guid>("ReservationId")
                        .HasColumnType("TEXT")
                        .HasColumnName("reservation_id");

                    b.Property<string>("SeatNumber")
                        .HasColumnType("TEXT")
                        .HasColumnName("seat_number");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.HasIndex("ReservationId")
                        .HasDatabaseName("ix_customers_reservation_id");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.FlightEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Airline")
                        .HasColumnType("TEXT")
                        .HasColumnName("airline");

                    b.Property<Guid>("ArrivalCityId")
                        .HasColumnType("TEXT")
                        .HasColumnName("arrival_city_id");

                    b.Property<DateTime?>("ArrivalDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("arrival_date");

                    b.Property<Guid>("DepartureCityId")
                        .HasColumnType("TEXT")
                        .HasColumnName("departure_city_id");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("departure_date");

                    b.Property<string>("FlightNumber")
                        .HasColumnType("TEXT")
                        .HasColumnName("flight_number");

                    b.Property<int>("NumberOfAvailableSeats")
                        .HasColumnType("INTEGER")
                        .HasColumnName("number_of_available_seats");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("TEXT")
                        .HasColumnName("plan_id");

                    b.Property<double>("Price")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("price");

                    b.HasKey("Id")
                        .HasName("pk_flights");

                    b.HasIndex("ArrivalCityId")
                        .HasDatabaseName("ix_flights_arrival_city_id");

                    b.HasIndex("DepartureCityId")
                        .HasDatabaseName("ix_flights_departure_city_id");

                    b.HasIndex("PlanId")
                        .HasDatabaseName("ix_flights_plan_id");

                    b.ToTable("flights", (string)null);
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.PlanEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("ModelName")
                        .HasColumnType("TEXT")
                        .HasColumnName("model_name");

                    b.HasKey("Id")
                        .HasName("pk_plans");

                    b.ToTable("plans", (string)null);
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.ReservationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateReservation")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_reservation");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("TEXT")
                        .HasColumnName("flight_id");

                    b.Property<int>("NumberOfPassengers")
                        .HasColumnType("INTEGER")
                        .HasColumnName("number_of_passengers");

                    b.Property<string>("SeatNumber")
                        .HasColumnType("TEXT")
                        .HasColumnName("seat_number");

                    b.HasKey("Id")
                        .HasName("pk_reservations");

                    b.HasIndex("FlightId")
                        .HasDatabaseName("ix_reservations_flight_id");

                    b.ToTable("reservations", (string)null);
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.SeatArrangementEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("TEXT")
                        .HasColumnName("flight_id");

                    b.Property<string>("SeatNumber")
                        .HasColumnType("TEXT")
                        .HasColumnName("seat_number");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_seat_arrangements");

                    b.HasIndex("FlightId")
                        .HasDatabaseName("ix_seat_arrangements_flight_id");

                    b.ToTable("seat_arrangements", (string)null);
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.CustomerEntity", b =>
                {
                    b.HasOne("SkyJourney.Infrastructure.Data.Models.ReservationEntity", "Reservation")
                        .WithMany("Passengers")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_customers_reservations_reservation_id");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.FlightEntity", b =>
                {
                    b.HasOne("SkyJourney.Infrastructure.Data.Models.CityEntity", "ArrivalCity")
                        .WithMany()
                        .HasForeignKey("ArrivalCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_flights_cities_arrival_city_id");

                    b.HasOne("SkyJourney.Infrastructure.Data.Models.CityEntity", "DepartureCity")
                        .WithMany()
                        .HasForeignKey("DepartureCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_flights_cities_departure_city_id");

                    b.HasOne("SkyJourney.Infrastructure.Data.Models.PlanEntity", "Plan")
                        .WithMany("Flights")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_flights_plans_plan_id");

                    b.Navigation("ArrivalCity");

                    b.Navigation("DepartureCity");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.ReservationEntity", b =>
                {
                    b.HasOne("SkyJourney.Infrastructure.Data.Models.FlightEntity", "Flight")
                        .WithMany("Reservations")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reservations_flights_flight_id");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.SeatArrangementEntity", b =>
                {
                    b.HasOne("SkyJourney.Infrastructure.Data.Models.FlightEntity", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_seat_arrangements_flights_flight_id");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.FlightEntity", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.PlanEntity", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("SkyJourney.Infrastructure.Data.Models.ReservationEntity", b =>
                {
                    b.Navigation("Passengers");
                });
#pragma warning restore 612, 618
        }
    }
}
