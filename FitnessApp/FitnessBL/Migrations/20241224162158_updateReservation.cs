using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class updateReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservation_equipment_equipment_id",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_reservation_time_slot_time_slot_id",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropIndex(
                name: "IX_reservation_equipment_id",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropIndex(
                name: "IX_reservation_time_slot_id",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropColumn(
                name: "equipment_id",
                schema: "dbo",
                table: "reservation");

            migrationBuilder.DropColumn(
                name: "time_slot_id",
                schema: "dbo",
                table: "reservation");

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

            migrationBuilder.CreateTable(
                name: "ReservationTimeslot",
                schema: "dbo",
                columns: table => new
                {
                    reservation_time_slot_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reservation_id = table.Column<int>(type: "int", nullable: false),
                    time_slot_id = table.Column<int>(type: "int", nullable: false),
                    equipment_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTimeslot", x => x.reservation_time_slot_id);
                    table.ForeignKey(
                        name: "FK_ReservationTimeslot_equipment_equipment_id",
                        column: x => x.equipment_id,
                        principalSchema: "dbo",
                        principalTable: "equipment",
                        principalColumn: "equipment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationTimeslot_reservation_reservation_id",
                        column: x => x.reservation_id,
                        principalSchema: "dbo",
                        principalTable: "reservation",
                        principalColumn: "reservation_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationTimeslot_time_slot_time_slot_id",
                        column: x => x.time_slot_id,
                        principalSchema: "dbo",
                        principalTable: "time_slot",
                        principalColumn: "time_slot_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTimeslot_equipment_id",
                schema: "dbo",
                table: "ReservationTimeslot",
                column: "equipment_id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTimeslot_reservation_id",
                schema: "dbo",
                table: "ReservationTimeslot",
                column: "reservation_id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTimeslot_time_slot_id",
                schema: "dbo",
                table: "ReservationTimeslot",
                column: "time_slot_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationTimeslot",
                schema: "dbo");

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

            migrationBuilder.CreateIndex(
                name: "IX_reservation_equipment_id",
                schema: "dbo",
                table: "reservation",
                column: "equipment_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_time_slot_id",
                schema: "dbo",
                table: "reservation",
                column: "time_slot_id");

            migrationBuilder.AddForeignKey(
                name: "FK_reservation_equipment_equipment_id",
                schema: "dbo",
                table: "reservation",
                column: "equipment_id",
                principalSchema: "dbo",
                principalTable: "equipment",
                principalColumn: "equipment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservation_time_slot_time_slot_id",
                schema: "dbo",
                table: "reservation",
                column: "time_slot_id",
                principalSchema: "dbo",
                principalTable: "time_slot",
                principalColumn: "time_slot_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
