using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyJourney.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseCitiesReferenceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_seat_arrangements_flights_flight_id",
                table: "seat_arrangements");

            migrationBuilder.DropColumn(
                name: "arrival_city",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "departure_city",
                table: "flights");

            migrationBuilder.AlterColumn<Guid>(
                name: "flight_id",
                table: "seat_arrangements",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "arrival_city_id",
                table: "flights",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "departure_city_id",
                table: "flights",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_flights_arrival_city_id",
                table: "flights",
                column: "arrival_city_id");

            migrationBuilder.CreateIndex(
                name: "ix_flights_departure_city_id",
                table: "flights",
                column: "departure_city_id");

            migrationBuilder.AddForeignKey(
                name: "fk_flights_cities_arrival_city_id",
                table: "flights",
                column: "arrival_city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_flights_cities_departure_city_id",
                table: "flights",
                column: "departure_city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_seat_arrangements_flights_flight_id",
                table: "seat_arrangements",
                column: "flight_id",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_flights_cities_arrival_city_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "fk_flights_cities_departure_city_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "fk_seat_arrangements_flights_flight_id",
                table: "seat_arrangements");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropIndex(
                name: "ix_flights_arrival_city_id",
                table: "flights");

            migrationBuilder.DropIndex(
                name: "ix_flights_departure_city_id",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "arrival_city_id",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "departure_city_id",
                table: "flights");

            migrationBuilder.AlterColumn<Guid>(
                name: "flight_id",
                table: "seat_arrangements",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "arrival_city",
                table: "flights",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "departure_city",
                table: "flights",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_seat_arrangements_flights_flight_id",
                table: "seat_arrangements",
                column: "flight_id",
                principalTable: "flights",
                principalColumn: "id");
        }
    }
}
