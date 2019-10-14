using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class claims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateFound",
                table: "LostItemClaims",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "LostItemClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LostItemClaims_ImageId",
                table: "LostItemClaims",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_LostItemClaims_Images_ImageId",
                table: "LostItemClaims",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LostItemClaims_Images_ImageId",
                table: "LostItemClaims");

            migrationBuilder.DropIndex(
                name: "IX_LostItemClaims_ImageId",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "DateFound",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "LostItemClaims");
        }
    }
}
