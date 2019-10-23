using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEditedLockOut",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "IsEditedLockOut",
                table: "FoundItemClaims");

            migrationBuilder.RenameColumn(
                name: "ExactAreaLost",
                table: "FoundItemClaims",
                newName: "WhereItemWasLost");

            migrationBuilder.AddColumn<int>(
                name: "IsEditedCount",
                table: "LostItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsEditedCount",
                table: "FoundItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "FoundItemClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEditedCount",
                table: "LostItems");

            migrationBuilder.DropColumn(
                name: "IsEditedCount",
                table: "FoundItems");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "FoundItemClaims");

            migrationBuilder.RenameColumn(
                name: "WhereItemWasLost",
                table: "FoundItemClaims",
                newName: "ExactAreaLost");

            migrationBuilder.AddColumn<bool>(
                name: "IsEditedLockOut",
                table: "LostItemClaims",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEditedLockOut",
                table: "FoundItemClaims",
                nullable: false,
                defaultValue: false);
        }
    }
}
