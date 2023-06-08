using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class RemoveRepliedComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_issue_comment_issue_comment_replied_comment_id",
                table: "issue_comment");

            migrationBuilder.DropIndex(
                name: "IX_issue_comment_replied_comment_id",
                table: "issue_comment");

            migrationBuilder.DropColumn(
                name: "replied_comment_id",
                table: "issue_comment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "replied_comment_id",
                table: "issue_comment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_issue_comment_replied_comment_id",
                table: "issue_comment",
                column: "replied_comment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_issue_comment_issue_comment_replied_comment_id",
                table: "issue_comment",
                column: "replied_comment_id",
                principalTable: "issue_comment",
                principalColumn: "id");
        }
    }
}
