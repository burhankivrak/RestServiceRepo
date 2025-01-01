using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class updatedReservatie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropColumn(
                name: "reserved_time_slots",
                schema: "dbo",
                table: "reservation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "dbo",
                table: "reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "dbo",
                table: "reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "dbo",
                table: "reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reserved_time_slots",
                schema: "dbo",
                table: "reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
