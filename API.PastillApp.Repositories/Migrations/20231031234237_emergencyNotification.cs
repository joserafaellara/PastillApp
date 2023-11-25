using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.PastillApp.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class emergencyNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmergencyNotification",
                table: "ReminderLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmergencyNotification",
                table: "ReminderLogs");
        }
    }
}
