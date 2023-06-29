using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class ProjectPermisiso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_permission_project_role_role_id",
                table: "project_permission");

            migrationBuilder.DropColumn(
                name: "description",
                table: "project_permission");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "project_permission",
                newName: "project_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_permission_role_id",
                table: "project_permission",
                newName: "IX_project_permission_project_role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_permission_project_role_project_role_id",
                table: "project_permission",
                column: "project_role_id",
                principalTable: "project_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_permission_project_role_project_role_id",
                table: "project_permission");

            migrationBuilder.RenameColumn(
                name: "project_role_id",
                table: "project_permission",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_permission_project_role_id",
                table: "project_permission",
                newName: "IX_project_permission_role_id");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "project_permission",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_project_permission_project_role_role_id",
                table: "project_permission",
                column: "role_id",
                principalTable: "project_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
