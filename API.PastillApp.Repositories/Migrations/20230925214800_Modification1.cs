using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.PastillApp.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Modification1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_EmergencyContactUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmergencyContactUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmergencyContactUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmergencyUserId",
                table: "Users",
                column: "EmergencyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_EmergencyUserId",
                table: "Users",
                column: "EmergencyUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_EmergencyUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmergencyUserId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "EmergencyContactUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmergencyContactUserId",
                table: "Users",
                column: "EmergencyContactUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_EmergencyContactUserId",
                table: "Users",
                column: "EmergencyContactUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
