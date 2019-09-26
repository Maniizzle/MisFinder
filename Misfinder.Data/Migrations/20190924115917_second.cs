using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "LostItems");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FoundItems",
                newName: "NameOfFoundItem");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "FoundItems",
                newName: "StateId");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "LostItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExactLocation",
                table: "FoundItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "FoundItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FoundItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemClaim_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemClaim_FoundItems_FoundItemId",
                        column: x => x.FoundItemId,
                        principalTable: "FoundItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalGovernments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalGovernments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalGovernments_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Abia" },
                    { 21, "Kebbi" },
                    { 22, "Kogi" },
                    { 23, "Kwara" },
                    { 24, "Lagos" },
                    { 25, "Nassarawa" },
                    { 26, "Niger" },
                    { 27, "Ogun" },
                    { 28, "Ondo" },
                    { 29, "Osun" },
                    { 30, "Oyo" },
                    { 31, "Plateau" },
                    { 32, "Rivers" },
                    { 33, "Sokoto" },
                    { 34, "Taraba" },
                    { 35, "Yobe" },
                    { 20, "Katsina" },
                    { 36, "Zamfara" },
                    { 19, "Kano" },
                    { 17, "Jigawa" },
                    { 2, "Adamawa" },
                    { 3, "Akwa Ibom" },
                    { 4, "Anambra" },
                    { 5, "Bauchi" },
                    { 6, "Bayelsa" },
                    { 7, "Benue" },
                    { 8, "Borno" },
                    { 9, "CrossRiver" },
                    { 10, "Delta" },
                    { 11, "Ebonyi" },
                    { 12, "Edo" },
                    { 13, "Ekiti" },
                    { 14, "Enugu" },
                    { 15, "Gombe" },
                    { 16, "Imo" },
                    { 18, "Kaduna" },
                    { 37, "Abuja" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LostItems_StateId",
                table: "LostItems",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundItems_StateId",
                table: "FoundItems",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemClaim_ApplicationUserId",
                table: "ItemClaim",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemClaim_FoundItemId",
                table: "ItemClaim",
                column: "FoundItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalGovernments_StateId",
                table: "LocalGovernments",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoundItems_States_StateId",
                table: "FoundItems",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LostItems_States_StateId",
                table: "LostItems",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoundItems_States_StateId",
                table: "FoundItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LostItems_States_StateId",
                table: "LostItems");

            migrationBuilder.DropTable(
                name: "ItemClaim");

            migrationBuilder.DropTable(
                name: "LocalGovernments");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropIndex(
                name: "IX_LostItems_StateId",
                table: "LostItems");

            migrationBuilder.DropIndex(
                name: "IX_FoundItems_StateId",
                table: "FoundItems");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "LostItems");

            migrationBuilder.DropColumn(
                name: "ExactLocation",
                table: "FoundItems");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "FoundItems");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "FoundItems",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "NameOfFoundItem",
                table: "FoundItems",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "LostItems",
                nullable: true);
        }
    }
}
