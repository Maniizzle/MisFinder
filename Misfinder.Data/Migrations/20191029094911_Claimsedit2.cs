using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class Claimsedit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ValidatedOn",
                table: "LostItemClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidatedOn",
                table: "FoundItemClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidatedOn",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "ValidatedOn",
                table: "FoundItemClaims");
        }
    }
}
