using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class MemberRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "member_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    project_member_id = table.Column<int>(type: "int", nullable: false),
                    project_role_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_member_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_member_role_project_member_project_member_id",
                        column: x => x.project_member_id,
                        principalTable: "project_member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_member_role_project_role_project_role_id",
                        column: x => x.project_role_id,
                        principalTable: "project_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_member_role_project_member_id",
                table: "member_role",
                column: "project_member_id");

            migrationBuilder.CreateIndex(
                name: "IX_member_role_project_role_id",
                table: "member_role",
                column: "project_role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_role");
        }
    }
}
