using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.PastillApp.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionReminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalDate",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Presentation",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "Frequency",
                table: "Reminders",
                newName: "IntakeTimeText");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Reminders",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "Reminders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyNumber",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FrequencyText",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IntakeTimeNumber",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Presentation",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RemindersDateTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeValue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReminderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemindersDateTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemindersDateTimes_Reminders_ReminderId",
                        column: x => x.ReminderId,
                        principalTable: "Reminders",
                        principalColumn: "ReminderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemindersDateTimes_ReminderId",
                table: "RemindersDateTimes",
                column: "ReminderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemindersDateTimes");

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "FrequencyNumber",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "FrequencyText",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "IntakeTimeNumber",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Presentation",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "IntakeTimeText",
                table: "Reminders",
                newName: "Frequency");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Reminders",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalDate",
                table: "Reminders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Presentation",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
