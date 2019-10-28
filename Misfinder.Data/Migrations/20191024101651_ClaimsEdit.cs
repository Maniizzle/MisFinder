using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class ClaimsEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "FoundItemClaims",
                newName: "IsValidated");

            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "LostItemClaims",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WhereItemWasFound",
                table: "LostItemClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "WhereItemWasFound",
                table: "LostItemClaims");

            migrationBuilder.RenameColumn(
                name: "IsValidated",
                table: "FoundItemClaims",
                newName: "IsValid");
        }
    }
}
