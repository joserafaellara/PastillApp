﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.PastillApp.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class reminderActivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reminders",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reminders");
        }
    }
}
