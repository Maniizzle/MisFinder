using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class metting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMeetingSucceess",
                table: "LostItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMeetingSucceess",
                table: "FoundItems",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMeetingSucceess",
                table: "LostItems");

            migrationBuilder.DropColumn(
                name: "IsMeetingSucceess",
                table: "FoundItems");
        }
    }
}
