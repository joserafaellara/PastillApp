using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.PastillApp.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class PushNotificationWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemindersDateTimes");

            migrationBuilder.AddColumn<bool>(
                name: "Notificated",
                table: "ReminderLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SecondNotification",
                table: "ReminderLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notificated",
                table: "ReminderLogs");

            migrationBuilder.DropColumn(
                name: "SecondNotification",
                table: "ReminderLogs");

            migrationBuilder.CreateTable(
                name: "RemindersDateTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReminderId = table.Column<int>(type: "int", nullable: false),
                    DateTimeValue = table.Column<DateTime>(type: "datetime2", nullable: false)
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
    }
}
