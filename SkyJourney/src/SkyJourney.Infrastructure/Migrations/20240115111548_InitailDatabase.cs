using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyJourney.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitailDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "plans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    model_name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_plans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    airline = table.Column<string>(type: "TEXT", nullable: true),
                    flight_number = table.Column<string>(type: "TEXT", nullable: true),
                    departure_city = table.Column<string>(type: "TEXT", nullable: true),
                    arrival_city = table.Column<string>(type: "TEXT", nullable: true),
                    departure_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    arrival_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    price = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    number_of_available_seats = table.Column<int>(type: "INTEGER", nullable: false),
                    plan_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flights", x => x.id);
                    table.ForeignKey(
                        name: "fk_flights_plans_plan_id",
                        column: x => x.plan_id,
                        principalTable: "plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    number_of_passengers = table.Column<int>(type: "INTEGER", nullable: false),
                    seat_number = table.Column<string>(type: "TEXT", nullable: true),
                    date_reservation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    flight_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservations", x => x.id);
                    table.ForeignKey(
                        name: "fk_reservations_flights_flight_id",
                        column: x => x.flight_id,
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seat_arrangements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    seat_number = table.Column<string>(type: "TEXT", nullable: true),
                    status = table.Column<bool>(type: "INTEGER", nullable: false),
                    flight_id = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_seat_arrangements", x => x.id);
                    table.ForeignKey(
                        name: "fk_seat_arrangements_flights_flight_id",
                        column: x => x.flight_id,
                        principalTable: "flights",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: true),
                    last_name = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    seat_number = table.Column<string>(type: "TEXT", nullable: true),
                    reservation_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                    table.ForeignKey(
                        name: "fk_customers_reservations_reservation_id",
                        column: x => x.reservation_id,
                        principalTable: "reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customers_reservation_id",
                table: "customers",
                column: "reservation_id");

            migrationBuilder.CreateIndex(
                name: "ix_flights_plan_id",
                table: "flights",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "ix_reservations_flight_id",
                table: "reservations",
                column: "flight_id");

            migrationBuilder.CreateIndex(
                name: "ix_seat_arrangements_flight_id",
                table: "seat_arrangements",
                column: "flight_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "seat_arrangements");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "plans");
        }
    }
}
