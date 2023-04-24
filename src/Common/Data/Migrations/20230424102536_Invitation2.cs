using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class Invitation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_accepted",
                table: "invitation");

            migrationBuilder.DropColumn(
                name: "is_declined",
                table: "invitation");

            migrationBuilder.AddColumn<DateTime>(
                name: "accepted_at",
                table: "invitation",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "declined_at",
                table: "invitation",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accepted_at",
                table: "invitation");

            migrationBuilder.DropColumn(
                name: "declined_at",
                table: "invitation");

            migrationBuilder.AddColumn<bool>(
                name: "is_accepted",
                table: "invitation",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_declined",
                table: "invitation",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
