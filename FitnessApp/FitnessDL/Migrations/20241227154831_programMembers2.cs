using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Migrations
{
    /// <inheritdoc />
    public partial class programMembers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_programmembers",
            //    schema: "dbo",
            //    table: "programmembers");

            //migrationBuilder.AlterColumn<int>(
            //    name: "member_id",
            //    schema: "dbo",
            //    table: "programmembers",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("Relational:ColumnOrder", 1);

            //migrationBuilder.AlterColumn<string>(
            //    name: "programCode",
            //    schema: "dbo",
            //    table: "programmembers",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)")
            //    .Annotation("Relational:ColumnOrder", 0);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_programmembers",
            //    schema: "dbo",
            //    table: "programmembers",
            //    columns: new[] { "programCode", "member_id" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_programmembers_member_id",
            //    schema: "dbo",
            //    table: "programmembers",
            //    column: "member_id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_programmembers_members_member_id",
            //    schema: "dbo",
            //    table: "programmembers",
            //    column: "member_id",
            //    principalSchema: "dbo",
            //    principalTable: "members",
            //    principalColumn: "member_id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_programmembers_program_programCode",
            //    schema: "dbo",
            //    table: "programmembers",
            //    column: "programCode",
            //    principalSchema: "dbo",
            //    principalTable: "program",
            //    principalColumn: "programCode",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_programmembers_members_member_id",
            //    schema: "dbo",
            //    table: "programmembers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_programmembers_program_programCode",
            //    schema: "dbo",
            //    table: "programmembers");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_programmembers",
            //    schema: "dbo",
            //    table: "programmembers");

            //migrationBuilder.DropIndex(
            //    name: "IX_programmembers_member_id",
            //    schema: "dbo",
            //    table: "programmembers");

            //migrationBuilder.AlterColumn<int>(
            //    name: "member_id",
            //    schema: "dbo",
            //    table: "programmembers",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("Relational:ColumnOrder", 1);

            //migrationBuilder.AlterColumn<string>(
            //    name: "programCode",
            //    schema: "dbo",
            //    table: "programmembers",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)")
            //    .OldAnnotation("Relational:ColumnOrder", 0);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_programmembers",
            //    schema: "dbo",
            //    table: "programmembers",
            //    column: "programCode");
        }
    }
}
