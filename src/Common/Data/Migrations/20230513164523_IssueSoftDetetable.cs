using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class IssueSoftDetetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "issue",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "issue");
        }
    }
}
