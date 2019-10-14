using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class ClaimsAdditiion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsEditedCount",
                table: "LostItemClaims",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEditedLockOut",
                table: "LostItemClaims",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLost",
                table: "FoundItemClaims",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExactAreaLost",
                table: "FoundItemClaims",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsEditedCount",
                table: "FoundItemClaims",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEditedLockOut",
                table: "FoundItemClaims",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEditedCount",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "IsEditedLockOut",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "DateLost",
                table: "FoundItemClaims");

            migrationBuilder.DropColumn(
                name: "ExactAreaLost",
                table: "FoundItemClaims");

            migrationBuilder.DropColumn(
                name: "IsEditedCount",
                table: "FoundItemClaims");

            migrationBuilder.DropColumn(
                name: "IsEditedLockOut",
                table: "FoundItemClaims");
        }
    }
}
