using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class statusupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "Sorted",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "FoundItemClaims");

            migrationBuilder.AlterColumn<byte>(
                name: "ItemCategory",
                table: "LostItemClaims",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "LostItemClaims",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "FoundItemClaims",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LostItemClaims");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FoundItemClaims");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCategory",
                table: "LostItemClaims",
                nullable: true,
                oldClrType: typeof(byte),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "LostItemClaims",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sorted",
                table: "LostItemClaims",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "FoundItemClaims",
                nullable: false,
                defaultValue: false);
        }
    }
}
