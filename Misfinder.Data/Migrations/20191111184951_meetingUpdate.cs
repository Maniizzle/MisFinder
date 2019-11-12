using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class meetingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocalGovernmentId",
                table: "Meetings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LocalGovernmentId",
                table: "Meetings",
                column: "LocalGovernmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_LocalGovernments_LocalGovernmentId",
                table: "Meetings",
                column: "LocalGovernmentId",
                principalTable: "LocalGovernments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_LocalGovernments_LocalGovernmentId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_LocalGovernmentId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "LocalGovernmentId",
                table: "Meetings");
        }
    }
}