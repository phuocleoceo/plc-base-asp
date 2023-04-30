using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class ProjectLeader2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "leader_id",
                table: "project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_project_leader_id",
                table: "project",
                column: "leader_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_user_account_leader_id",
                table: "project",
                column: "leader_id",
                principalTable: "user_account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_user_account_leader_id",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_leader_id",
                table: "project");

            migrationBuilder.DropColumn(
                name: "leader_id",
                table: "project");
        }
    }
}
