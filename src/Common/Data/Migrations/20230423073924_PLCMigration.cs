using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class PLCMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "issue",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    story_point = table.Column<double>(type: "double", nullable: false),
                    priority = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    backlog_index = table.Column<int>(type: "int", nullable: true),
                    sprint_id = table.Column<int>(type: "int", nullable: true),
                    reporter_id = table.Column<int>(type: "int", nullable: false),
                    assignee_id = table.Column<int>(type: "int", nullable: true),
                    project_status_id = table.Column<int>(type: "int", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue", x => x.id);
                    table.ForeignKey(
                        name: "FK_issue_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_issue_project_status_project_status_id",
                        column: x => x.project_status_id,
                        principalTable: "project_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_issue_sprint_sprint_id",
                        column: x => x.sprint_id,
                        principalTable: "sprint",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_issue_user_account_assignee_id",
                        column: x => x.assignee_id,
                        principalTable: "user_account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_issue_user_account_reporter_id",
                        column: x => x.reporter_id,
                        principalTable: "user_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_issue_assignee_id",
                table: "issue",
                column: "assignee_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue_project_id_project_status_id_sprint_id_reporter_id_ass~",
                table: "issue",
                columns: new[] { "project_id", "project_status_id", "sprint_id", "reporter_id", "assignee_id" });

            migrationBuilder.CreateIndex(
                name: "IX_issue_project_status_id",
                table: "issue",
                column: "project_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue_reporter_id",
                table: "issue",
                column: "reporter_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue_sprint_id",
                table: "issue",
                column: "sprint_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "issue");
        }
    }
}
