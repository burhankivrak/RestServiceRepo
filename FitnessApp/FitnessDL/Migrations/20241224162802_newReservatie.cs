using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class newReservatie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "reservation$FK_reservation_equipment",
                schema: "dbo",
                table: "reservation");
            migrationBuilder.DropForeignKey(
               name: "reservation$FK_reservation_time_slot",
               schema: "dbo",
               table: "reservation");
            // Now drop the columns
            migrationBuilder.DropColumn(
                name: "equipment_id",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropColumn(
                name: "time_slot_id",
                schema: "dbo",
                table: "reservation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "equipment_id",
                schema: "dbo",
                table: "reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "time_slot_id",
                schema: "dbo",
                table: "reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Recreate the foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_reservation_equipment_equipment_id",
                schema: "dbo",
                table: "reservation",
                column: "equipment_id",
                principalSchema: "dbo",
                principalTable: "equipment",
                principalColumn: "equipment_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

