using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class meetingUpdate22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSelectedDate",
                table: "Meetings",
                newName: "IsUserTwoSelectedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUserTwoSelectedDate",
                table: "Meetings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUserTwoSelectedDate",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "IsUserTwoSelectedDate",
                table: "Meetings",
                newName: "IsSelectedDate");
        }
    }
}
