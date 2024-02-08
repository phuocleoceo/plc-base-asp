using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plcbase.Common.Data.Migrations
{
    public partial class PasswordString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_hashed",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "password_salt",
                table: "user_account");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "user_account",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "user_account");

            migrationBuilder.AddColumn<byte[]>(
                name: "password_hashed",
                table: "user_account",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "password_salt",
                table: "user_account",
                type: "longblob",
                nullable: true);
        }
    }
}
