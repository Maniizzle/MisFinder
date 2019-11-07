using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class amlost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Meetings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Meetings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IsEditedCount",
                table: "Meetings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Meetings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedCount",
                table: "Meetings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "IsEditedCount",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "SelectedCount",
                table: "Meetings");
        }
    }
}
