using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class ProjectStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_project_member_user_id_project_id",
                table: "project_member");

            migrationBuilder.DropIndex(
                name: "IX_project_creator_id",
                table: "project");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "user_profile",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "identity_number",
                table: "user_profile",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "project_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_status", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_status_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_user_id_identity_number_phone_number",
                table: "user_profile",
                columns: new[] { "user_id", "identity_number", "phone_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_member_user_id_project_id",
                table: "project_member",
                columns: new[] { "user_id", "project_id" });

            migrationBuilder.CreateIndex(
                name: "IX_project_creator_id",
                table: "project",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_status_project_id",
                table: "project_status",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_status");

            migrationBuilder.DropIndex(
                name: "IX_user_profile_user_id_identity_number_phone_number",
                table: "user_profile");

            migrationBuilder.DropIndex(
                name: "IX_project_member_user_id_project_id",
                table: "project_member");

            migrationBuilder.DropIndex(
                name: "IX_project_creator_id",
                table: "project");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "user_profile",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "identity_number",
                table: "user_profile",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_project_member_user_id_project_id",
                table: "project_member",
                columns: new[] { "user_id", "project_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_creator_id",
                table: "project",
                column: "creator_id",
                unique: true);
        }
    }
}
