using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class IssueComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "issue_comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    replied_comment_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    issue_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_issue_comment_issue_comment_replied_comment_id",
                        column: x => x.replied_comment_id,
                        principalTable: "issue_comment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_issue_comment_issue_issue_id",
                        column: x => x.issue_id,
                        principalTable: "issue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_issue_comment_user_account_user_id",
                        column: x => x.user_id,
                        principalTable: "user_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_issue_comment_issue_id",
                table: "issue_comment",
                column: "issue_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue_comment_replied_comment_id",
                table: "issue_comment",
                column: "replied_comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue_comment_user_id",
                table: "issue_comment",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "issue_comment");
        }
    }
}
