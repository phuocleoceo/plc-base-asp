using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class IssueIndexInStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "index",
                table: "project_status",
                type: "double",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "backlog_index",
                table: "issue",
                type: "double",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "project_status_index",
                table: "issue",
                type: "double",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "project_status_index",
                table: "issue");

            migrationBuilder.AlterColumn<int>(
                name: "index",
                table: "project_status",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<int>(
                name: "backlog_index",
                table: "issue",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);
        }
    }
}
