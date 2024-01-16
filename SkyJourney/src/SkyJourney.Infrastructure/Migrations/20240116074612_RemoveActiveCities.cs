using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyJourney.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveActiveCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "cities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "cities",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
