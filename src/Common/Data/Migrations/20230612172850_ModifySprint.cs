using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class ModifySprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_in_progress",
                table: "sprint");

            migrationBuilder.RenameColumn(
                name: "start_time",
                table: "sprint",
                newName: "to_date");

            migrationBuilder.RenameColumn(
                name: "end_time",
                table: "sprint",
                newName: "started_at");

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_at",
                table: "sprint",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "from_date",
                table: "sprint",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completed_at",
                table: "sprint");

            migrationBuilder.DropColumn(
                name: "from_date",
                table: "sprint");

            migrationBuilder.RenameColumn(
                name: "to_date",
                table: "sprint",
                newName: "start_time");

            migrationBuilder.RenameColumn(
                name: "started_at",
                table: "sprint",
                newName: "end_time");

            migrationBuilder.AddColumn<bool>(
                name: "is_in_progress",
                table: "sprint",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
