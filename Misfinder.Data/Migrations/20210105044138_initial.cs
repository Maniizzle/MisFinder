using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MisFinder.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    IsBlackListed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalGovernments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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

            migrationBuilder.CreateTable(
                name: "FoundItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsEditedCount = table.Column<int>(nullable: false),
                    ItemCategory = table.Column<string>(nullable: true),
                    NameOfFoundItem = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    LocalGovernmentId = table.Column<int>(nullable: false),
                    ExactArea = table.Column<string>(nullable: true),
                    WhereItemWasFound = table.Column<string>(maxLength: 100, nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    DateFound = table.Column<DateTime>(nullable: false),
                    ImageId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    IsClaimed = table.Column<bool>(nullable: false),
                    IsMeetingSucceess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoundItems_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoundItems_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoundItems_LocalGovernments_LocalGovernmentId",
                        column: x => x.LocalGovernmentId,
                        principalTable: "LocalGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LostItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsEditedCount = table.Column<int>(nullable: false),
                    ItemCategory = table.Column<string>(nullable: true),
                    NameOfLostItem = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    ExactArea = table.Column<string>(nullable: true),
                    LocalGovernmentId = table.Column<int>(nullable: false),
                    WhereItemWasLost = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    DateMisplaced = table.Column<DateTime>(nullable: false),
                    ImageId = table.Column<int>(nullable: true),
                    IsMeetingSucceess = table.Column<bool>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    IsFound = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LostItems_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LostItems_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LostItems_LocalGovernments_LocalGovernmentId",
                        column: x => x.LocalGovernmentId,
                        principalTable: "LocalGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoundItemClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsEditedCount = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    ItemCategory = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FoundItemId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: true),
                    WhereItemWasLost = table.Column<string>(nullable: true),
                    DateLost = table.Column<DateTime>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    ValidatedOn = table.Column<DateTime>(nullable: true),
                    IsAdminValid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundItemClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoundItemClaims_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoundItemClaims_FoundItems_FoundItemId",
                        column: x => x.FoundItemId,
                        principalTable: "FoundItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoundItemClaims_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LostItemClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsEditedCount = table.Column<int>(nullable: false),
                    ItemCategory = table.Column<byte>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    LostItemId = table.Column<int>(nullable: false),
                    WhereItemWasFound = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    ValidatedOn = table.Column<DateTime>(nullable: true),
                    DateFound = table.Column<DateTime>(nullable: false),
                    ImageId = table.Column<int>(nullable: true),
                    IsAdminValid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostItemClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LostItemClaims_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LostItemClaims_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LostItemClaims_LostItems_LostItemId",
                        column: x => x.LostItemId,
                        principalTable: "LostItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsEditedCount = table.Column<int>(nullable: false),
                    UserSelectedDate = table.Column<DateTime>(nullable: false),
                    USerSelectedDate2 = table.Column<DateTime>(nullable: false),
                    IsSelectFirstDate = table.Column<bool>(nullable: false),
                    IsSelectSecondDate = table.Column<bool>(nullable: false),
                    MeetingVenue = table.Column<string>(nullable: true),
                    IsUserTwoSelectedDate = table.Column<bool>(nullable: false),
                    DateUserTwoSelectedDate = table.Column<DateTime>(nullable: true),
                    MeeetingTime = table.Column<DateTime>(nullable: false),
                    LostItemId = table.Column<int>(nullable: true),
                    FoundItemId = table.Column<int>(nullable: true),
                    SelectedCount = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    LocalGovernmentId = table.Column<int>(nullable: false),
                    IsAdminIncluded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_FoundItems_FoundItemId",
                        column: x => x.FoundItemId,
                        principalTable: "FoundItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_LocalGovernments_LocalGovernmentId",
                        column: x => x.LocalGovernmentId,
                        principalTable: "LocalGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meetings_LostItems_LostItemId",
                        column: x => x.LostItemId,
                        principalTable: "LostItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", null, "User", "User" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
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
                    { 19, "Kano" },
                    { 18, "Kaduna" },
                    { 17, "Jigawa" },
                    { 1, "Abia" },
                    { 2, "Adamawa" },
                    { 3, "Akwa Ibom" },
                    { 4, "Anambra" },
                    { 5, "Bauchi" },
                    { 6, "Bayelsa" },
                    { 7, "Benue" },
                    { 36, "Zamfara" },
                    { 8, "Borno" },
                    { 10, "Delta" },
                    { 11, "Ebonyi" },
                    { 12, "Edo" },
                    { 13, "Ekiti" },
                    { 14, "Enugu" },
                    { 15, "Gombe" },
                    { 16, "Imo" },
                    { 9, "CrossRiver" },
                    { 37, "Abuja" }
                });

            migrationBuilder.InsertData(
                table: "LocalGovernments",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 66, "Aba North", 1 },
                    { 15, "Ajeromi-Ifelodun", 24 },
                    { 16, "Amuwo-Odofin", 24 },
                    { 17, "Badagry", 24 },
                    { 18, "Ikorodu", 24 },
                    { 19, "Ibeju-Lekki", 24 },
                    { 20, "Epe", 24 },
                    { 532, "Akwanga", 25 },
                    { 533, "Awe", 25 },
                    { 534, "Doma", 25 },
                    { 535, "Karu", 25 },
                    { 536, "Keana", 25 },
                    { 537, "Keffi", 25 },
                    { 538, "Kokona", 25 },
                    { 539, "Lafia", 25 },
                    { 540, "Nassarawa", 25 },
                    { 541, "Nassarawa-Egbon", 25 },
                    { 542, "Obi", 25 },
                    { 543, "Toto", 25 },
                    { 544, "Wamba", 25 },
                    { 14, "Ojo", 24 },
                    { 13, "Surulere", 24 },
                    { 12, "Lagos Mainland", 24 },
                    { 11, "LagosIsland", 24 },
                    { 522, "Ilorin South", 23 },
                    { 523, "Ilorin West", 23 },
                    { 524, "Irepodun", 23 },
                    { 525, "Isin", 23 },
                    { 526, "Kaiama", 23 },
                    { 527, "Moro", 23 },
                    { 528, "Offa", 23 },
                    { 529, "Oke Ero", 23 },
                    { 530, "Oyun", 23 },
                    { 545, "Agaie", 26 },
                    { 531, "Patigi", 23 },
                    { 2, "Alimosho", 24 },
                    { 3, "Apapa", 24 },
                    { 4, "Ifako-Ijaye", 24 },
                    { 5, "Ikeja", 24 },
                    { 6, "Kosofe", 24 },
                    { 7, "Mushin", 24 },
                    { 8, "Oshodi-Isolo", 24 },
                    { 9, "Shomolu", 24 },
                    { 10, "Eti-Osa", 24 },
                    { 1, "Agege", 24 },
                    { 521, "Ilorin East", 23 },
                    { 546, "Agwara", 26 },
                    { 548, "Borgu", 26 },
                    { 24, "Ifo", 27 },
                    { 25, "Ijebu East", 27 },
                    { 26, "Ijebu North", 27 },
                    { 27, "Ijebu North-East", 27 },
                    { 28, "Ijebu Ode", 27 },
                    { 29, "Ikenne", 27 },
                    { 30, "Imeko Afon", 27 },
                    { 31, "Ipokia", 27 },
                    { 32, "Obafemi Owode", 27 },
                    { 33, "Odogbolu", 27 },
                    { 34, "Odeda", 27 },
                    { 35, "Ogun Waterside", 27 },
                    { 36, "Remo North", 27 },
                    { 37, "Sagamu", 27 },
                    { 38, "Yewa North", 27 },
                    { 39, "Yewa South", 27 },
                    { 570, "Akoko North-East", 28 },
                    { 571, "Akoko North-West", 28 },
                    { 572, "Akoko South-East", 28 },
                    { 23, "Ewekoro", 27 },
                    { 22, "Ado-Odo/Ota", 27 },
                    { 21, "Abeokuta North", 27 },
                    { 569, "Wushishi", 26 },
                    { 549, "Bosso", 26 },
                    { 550, "Chanchaga", 26 },
                    { 551, "Edati", 26 },
                    { 552, "Gbako", 26 },
                    { 553, "Gurara", 26 },
                    { 554, "Katcha", 26 },
                    { 555, "Kontagora", 26 },
                    { 556, "Lapai", 26 },
                    { 557, "Lavun", 26 },
                    { 547, "Bida", 26 },
                    { 558, "Magama", 26 },
                    { 560, "Mashegu", 26 },
                    { 561, "Mokwa", 26 },
                    { 562, "Munya", 26 },
                    { 563, "Paikoro", 26 },
                    { 564, "Rafi", 26 },
                    { 565, "Rijau", 26 },
                    { 566, "Shiroro", 26 },
                    { 567, "Suleja", 26 },
                    { 568, "Tafa", 26 },
                    { 559, "Mariga", 26 },
                    { 520, "Ifolodun", 23 },
                    { 519, "Ekiti", 23 },
                    { 518, "Edu", 23 },
                    { 450, "Dutsin-Ma", 20 },
                    { 451, "Faskari", 20 },
                    { 452, "Funtua", 20 },
                    { 453, "Ingawa", 20 },
                    { 454, "Jibia", 20 },
                    { 455, "Kafur", 20 },
                    { 456, "Kaita", 20 },
                    { 457, "Kankara", 20 },
                    { 458, "Kankia", 20 },
                    { 459, "Katsina", 20 },
                    { 460, "Kurfi", 20 },
                    { 461, "Kusada", 20 },
                    { 462, "Mai'Adua", 20 },
                    { 463, "Malumfashi", 20 },
                    { 464, "Mani", 20 },
                    { 465, "Mashi", 20 },
                    { 466, "Matazu", 20 },
                    { 467, "Musawa", 20 },
                    { 468, "Rimi", 20 },
                    { 449, "Dutsi", 20 },
                    { 448, "Daura", 20 },
                    { 447, "Danja", 20 },
                    { 446, "Dandume", 20 },
                    { 426, "Rano", 19 },
                    { 427, "Rimin Gado", 19 },
                    { 428, "Rogo", 19 },
                    { 429, "Shanono", 19 },
                    { 430, "Sumaila", 19 },
                    { 431, "Takai", 19 },
                    { 432, "Tarauni", 19 },
                    { 433, "Tofa", 19 },
                    { 434, "Tsanyawa", 19 },
                    { 469, "Sabuwa", 20 },
                    { 435, "Tudun Wada", 19 },
                    { 437, "Warawa", 19 },
                    { 438, "Wudil", 19 },
                    { 439, "Bakori", 20 },
                    { 440, "Batagarawa", 20 },
                    { 441, "Batsari", 20 },
                    { 442, "Baure", 20 },
                    { 443, "Bindawa", 20 },
                    { 444, "Charanchi", 20 },
                    { 445, "Dan Musa", 20 },
                    { 436, "Ungogo", 19 },
                    { 470, "Safana", 20 },
                    { 471, "Sandanu", 20 },
                    { 472, "Zango", 20 },
                    { 498, "Bassa", 22 },
                    { 499, "Dekina", 22 },
                    { 500, "Ibaji", 22 },
                    { 501, "Idah", 22 },
                    { 502, "Igalamela-Odulu", 22 },
                    { 503, "Ijumu", 22 },
                    { 504, "Kabba/Bunu", 22 },
                    { 505, "Koton Karfe", 22 },
                    { 506, "Lokoja", 22 },
                    { 497, "Ankpa", 22 },
                    { 507, "Mopa-Muro", 22 },
                    { 509, "Ogori/Magongo", 22 },
                    { 510, "Okehi", 22 },
                    { 511, "Okene", 22 },
                    { 512, "Olamaboro", 22 },
                    { 513, "Omala", 22 },
                    { 514, "Yagba East", 22 },
                    { 515, "Yagba West", 22 },
                    { 516, "Asa", 23 },
                    { 517, "Baruten", 23 },
                    { 508, "Ofu", 22 },
                    { 573, "Akoko South-West", 28 },
                    { 496, "Ajaokuta", 22 },
                    { 494, "Zuru", 21 },
                    { 473, "Aliero", 21 },
                    { 474, "Arewa Dandi", 21 },
                    { 475, "Argungu", 21 },
                    { 476, "Augie", 21 },
                    { 477, "Bagudo", 21 },
                    { 478, "Birnin Kebbi", 21 },
                    { 480, "Bunza", 21 },
                    { 481, "Dandi", 21 },
                    { 482, "Fakai", 21 },
                    { 495, "Adavi", 22 },
                    { 483, "Gwandu", 21 },
                    { 485, "Kalgo", 21 },
                    { 486, "Koko/ Besse", 21 },
                    { 487, "Maiyama", 21 },
                    { 488, "Ngaski", 21 },
                    { 489, "Sakaba", 21 },
                    { 490, "Shanga", 21 },
                    { 491, "Suru", 21 },
                    { 492, "Danko/Wasagu", 21 },
                    { 493, "Yauri", 21 },
                    { 484, "Jega", 21 },
                    { 425, "Nasarawa", 19 },
                    { 574, "Akure North", 28 },
                    { 576, "Ese Odo", 28 },
                    { 696, "Illela", 33 },
                    { 697, "Isa", 33 },
                    { 698, "Kebbe", 33 },
                    { 699, "Kware", 33 },
                    { 700, "Rabah", 33 },
                    { 701, "Sabon Birni", 33 },
                    { 702, "Shagari", 33 },
                    { 703, "Silame", 33 },
                    { 704, "Sokoto North", 33 },
                    { 705, "Sokoto South", 33 },
                    { 706, "Tambuwal", 33 },
                    { 707, "Tangaza", 33 },
                    { 708, "Tureta", 33 },
                    { 709, "Wamako", 33 },
                    { 710, "Wurno", 33 },
                    { 711, "Yabo", 33 },
                    { 712, "Ardo Kola", 34 },
                    { 713, "Bali", 34 },
                    { 714, "Donga", 34 },
                    { 695, "GWadabawa", 33 },
                    { 694, "Gudu", 33 },
                    { 693, "Goronyo", 33 },
                    { 692, "Gada", 33 },
                    { 672, "Bonny", 32 },
                    { 673, "Degema", 32 },
                    { 674, "Emohua", 32 },
                    { 675, "Eleme", 32 },
                    { 676, "Etche", 32 },
                    { 677, "Gokana", 32 },
                    { 678, "Ikwerre", 32 },
                    { 679, "Khana", 32 },
                    { 680, "Obio/Akpor", 32 },
                    { 715, "Gashaka", 34 },
                    { 681, "Ogba/Egbema/Ndoni", 32 },
                    { 683, "Okrika", 32 },
                    { 684, "Omuwa", 32 },
                    { 685, "Opobo Nkoro", 32 },
                    { 686, "Oyigbo", 32 },
                    { 687, "Port Harcourt", 32 },
                    { 688, "Tai", 32 },
                    { 689, "Binji", 33 },
                    { 690, "Bodinga", 33 },
                    { 691, "Dange Shuni", 33 },
                    { 682, "Ogu/Bolo", 32 },
                    { 671, "Asari-Toru", 32 },
                    { 716, "Gassol", 34 },
                    { 718, "Jalingo", 34 },
                    { 744, "Yunusari", 35 },
                    { 745, "Yusufari", 35 },
                    { 746, "Anka", 36 },
                    { 747, "Bakura", 36 },
                    { 748, "Birnin Magaji/Kiyaw", 36 },
                    { 749, "Bukkuyum", 36 },
                    { 750, "Bungudu", 36 },
                    { 751, "Tsafe", 36 },
                    { 752, "Gummi", 36 },
                    { 753, "Gusau", 36 },
                    { 754, "Kaura Namoda", 36 },
                    { 755, "Maradun", 36 },
                    { 756, "Maru", 36 },
                    { 757, "Shinkafi", 36 },
                    { 758, "Talata Mafara", 36 },
                    { 759, "Zurmi", 36 },
                    { 40, "Gwagwalada", 37 },
                    { 41, "Abaji", 37 },
                    { 42, "Abuja Municipal", 37 },
                    { 743, "Tarmuwa", 35 },
                    { 742, "Potiskum", 35 },
                    { 741, "Nguru", 35 },
                    { 740, "Nangere", 35 },
                    { 719, "Karim Lamido", 34 },
                    { 720, "Kurmi", 34 },
                    { 721, "Lau", 34 },
                    { 722, "Sardauna", 34 },
                    { 723, "Takum", 34 },
                    { 724, "Ussa", 34 },
                    { 725, "Wukari", 34 },
                    { 726, "Yoro", 34 },
                    { 727, "Zing", 34 },
                    { 717, "Ibi", 34 },
                    { 728, "Bade", 35 },
                    { 730, "Damaturu", 35 },
                    { 731, "Geidam", 35 },
                    { 732, "Gujba", 35 },
                    { 733, "Gulani", 35 },
                    { 734, "Fika", 35 },
                    { 735, "Fune", 35 },
                    { 736, "Jakusko", 35 },
                    { 737, "Karasuwa", 35 },
                    { 738, "Machina", 35 },
                    { 729, "Busari", 35 },
                    { 670, "Andoni", 32 },
                    { 669, "Akuku Toru", 32 },
                    { 668, "Ahuoda West", 32 },
                    { 601, "Ifedayo", 29 },
                    { 602, "Ifelodun", 29 },
                    { 603, "Ila", 29 },
                    { 604, "Ilesha East", 29 },
                    { 605, "Ilesha West", 29 },
                    { 606, "Irepodun", 29 },
                    { 607, "Irewole", 29 },
                    { 608, "Isokan", 29 },
                    { 609, "Iwo", 29 },
                    { 610, "Obokun", 29 },
                    { 611, "Odo-Otin", 29 },
                    { 612, "Ola-Oluwa", 29 },
                    { 613, "Olorunda", 29 },
                    { 614, "Oriade", 29 },
                    { 615, "Orolu", 29 },
                    { 616, "Osogbo", 29 },
                    { 617, "Akinyele", 30 },
                    { 618, "Afijio", 30 },
                    { 619, "Atiba", 30 },
                    { 600, "Ife South", 29 },
                    { 599, "Ife North", 29 },
                    { 598, "Ife East", 29 },
                    { 597, "Ife Central", 29 },
                    { 577, "Idanre", 28 },
                    { 578, "Ifedore", 28 },
                    { 579, "Ilaje", 28 },
                    { 580, "Ile Oluji/OKeigbo", 28 },
                    { 581, "Irele", 28 },
                    { 582, "Odigbo", 28 },
                    { 583, "Okitipupa", 28 },
                    { 584, "Ondo West", 28 },
                    { 585, "Ose", 28 },
                    { 620, "Akisbo", 30 },
                    { 586, "Owo", 28 },
                    { 588, "Aiyedire", 29 },
                    { 589, "Atakumosa East", 29 },
                    { 590, "Atakumosa West", 29 },
                    { 591, "Boluwaduro", 29 },
                    { 592, "Boripe", 29 },
                    { 593, "Ede North", 29 },
                    { 594, "Ede South", 29 },
                    { 595, "Egbedore", 29 },
                    { 596, "Ejigbo", 29 },
                    { 587, "Aiyedaade", 29 },
                    { 621, "Egbeda", 30 },
                    { 622, "Ibadab North", 30 },
                    { 623, "Ibadan North-East", 30 },
                    { 648, "Saki East", 30 },
                    { 649, "Surulere", 30 },
                    { 650, "Barkin Ladi", 31 },
                    { 651, "Bokkos", 31 },
                    { 652, "Jos East", 31 },
                    { 653, "Jos North", 31 },
                    { 654, "Jos South", 31 },
                    { 655, "Kanam", 31 },
                    { 656, "Kanke", 31 },
                    { 647, "Saki West", 30 },
                    { 657, "Langtang North", 31 },
                    { 659, "Mangu", 31 },
                    { 660, "Mikang", 31 },
                    { 661, "Panshin", 31 },
                    { 662, "Qua'an Pan", 31 },
                    { 663, "Riyom", 31 },
                    { 664, "Shendam", 31 },
                    { 665, "Wase", 31 },
                    { 666, "Abua/Odual", 32 },
                    { 667, "Ahoada East", 32 },
                    { 658, "Langtang South", 31 },
                    { 575, "Akure South", 28 },
                    { 646, "Ona Ara", 30 },
                    { 644, "Ori Ire", 30 },
                    { 624, "Ibadan North-West", 30 },
                    { 625, "Ibadan South-West", 30 },
                    { 626, "Ibadan South-East", 30 },
                    { 627, "Ibarapa Central", 30 },
                    { 628, "Ibarapa East", 30 },
                    { 629, "Ido", 30 },
                    { 630, "Irepo", 30 },
                    { 631, "Iseyin", 30 },
                    { 632, "Itesiwaju", 30 },
                    { 645, "Oyo East", 30 },
                    { 633, "Iwajowa", 30 },
                    { 635, "Kajola", 30 },
                    { 636, "Lagelu", 30 },
                    { 637, "Ogbomosho North", 30 },
                    { 638, "Ogbomosho South", 30 },
                    { 639, "Oyo West", 30 },
                    { 640, "Olorunsogo", 30 },
                    { 641, "Oluyole", 30 },
                    { 642, "Ogo Oluwa", 30 },
                    { 643, "Orelope", 30 },
                    { 634, "Ibarapa North", 30 },
                    { 43, "Bwari", 37 },
                    { 424, "Minjibir", 19 },
                    { 422, "Madobi", 19 },
                    { 164, "Makurdi", 7 },
                    { 165, "Obi", 7 },
                    { 166, "Ogbadibo", 7 },
                    { 167, "Ohimmini", 7 },
                    { 168, "Oju", 7 },
                    { 169, "Okpokwu", 7 },
                    { 170, "Otukpo", 7 },
                    { 171, "Tarka", 7 },
                    { 172, "Ukum", 7 },
                    { 173, "Ushongo", 7 },
                    { 174, "Vandeikya", 7 },
                    { 175, "Maigugri", 8 },
                    { 176, "Ngala", 8 },
                    { 177, "Kala/Balge", 8 },
                    { 178, "Mafa", 8 },
                    { 179, "Konduga", 8 },
                    { 180, "Bama", 8 },
                    { 181, "Jere", 8 },
                    { 182, "Dikwa", 8 },
                    { 163, "Logo", 7 },
                    { 162, "Kwande", 7 },
                    { 161, "KOnshiha", 7 },
                    { 160, "Katsina Ala", 7 },
                    { 141, "Katagum", 5 },
                    { 142, "Itas/Gadau", 5 },
                    { 143, "Zaki", 5 },
                    { 144, "Gamawa", 5 },
                    { 145, "Brass", 6 },
                    { 146, "Ekeremor", 6 },
                    { 147, "Kolokuma/Opokuma", 6 },
                    { 148, "Nembe", 6 },
                    { 149, "Ogbia", 6 },
                    { 183, "Askira/Uba", 8 },
                    { 150, "Sagbama", 6 },
                    { 319, "Yenagoa", 6 },
                    { 152, "Ado", 7 },
                    { 153, "Agatu", 7 },
                    { 154, "Apa", 7 },
                    { 155, "Buruku", 7 },
                    { 156, "Gboko", 7 },
                    { 157, "Guma", 7 },
                    { 158, "Gwer East", 7 },
                    { 159, "Gwer Wast", 7 },
                    { 151, "Southern Ijaw", 6 },
                    { 140, "Jama'are", 5 },
                    { 184, "Bayo", 8 },
                    { 186, "Chibok", 8 },
                    { 211, "Ikom", 9 },
                    { 212, "Obanliku", 9 },
                    { 213, "Obubra", 9 },
                    { 214, "Obudu", 9 },
                    { 215, "Odukpani", 9 },
                    { 216, "Ogoja", 9 },
                    { 217, "Yakuur", 9 },
                    { 218, "Yala", 9 },
                    { 219, "Ethiope East", 10 },
                    { 220, "Ethiope West", 10 },
                    { 221, "Okpe", 10 },
                    { 222, "Sapele", 10 },
                    { 223, "Udu", 10 },
                    { 224, "Ughelli North", 10 },
                    { 225, "Uvwie", 10 },
                    { 226, "Aniocha North", 10 },
                    { 227, "Aniocha South", 10 },
                    { 228, "Ika North East", 10 },
                    { 230, "Ika South", 10 },
                    { 210, "Etung", 9 },
                    { 209, "Calabar South", 9 },
                    { 208, "Calar Municipal", 9 },
                    { 207, "Boki", 9 },
                    { 187, "Damboa", 8 },
                    { 188, "Gwoza", 8 },
                    { 189, "Hawul", 8 },
                    { 190, "Kwaya Kusar", 8 },
                    { 191, "Shani", 8 },
                    { 192, "Abadam", 8 },
                    { 193, "Gubio", 8 },
                    { 194, "Guzamala", 8 },
                    { 195, "Kaga", 8 },
                    { 185, "Biu", 8 },
                    { 196, "Maumeri", 8 },
                    { 198, "Mobbar", 8 },
                    { 199, "Monguno", 8 },
                    { 200, "Nganzai", 8 },
                    { 201, "Abi", 9 },
                    { 202, "Akamkpa", 9 },
                    { 203, "Akpabuyo", 9 },
                    { 204, "Bakassi", 9 },
                    { 205, "Bekwarra", 9 },
                    { 206, "Biase", 9 },
                    { 197, "Marte", 8 },
                    { 139, "Shira", 5 },
                    { 138, "Giade", 5 },
                    { 137, "Misau", 5 },
                    { 61, "Shelling", 2 },
                    { 62, "Song", 2 },
                    { 63, "Toungo", 2 },
                    { 64, "Yola North", 2 },
                    { 65, "Yola South", 2 },
                    { 75, "Abak", 3 },
                    { 76, "Eastern Obolo", 3 },
                    { 77, "Eket", 3 },
                    { 78, "Esit-Eket", 3 },
                    { 79, "Essien-Ekpo", 3 },
                    { 80, "Etim-Ukpom", 3 },
                    { 81, "Etinan", 3 },
                    { 82, "Ibeemo", 3 },
                    { 83, "Ibesikpo - Asutan", 3 },
                    { 84, "Ibiono-Ibom", 3 },
                    { 85, "Ika", 3 },
                    { 86, "Ikono", 3 },
                    { 87, "Ikot-Abasi", 3 },
                    { 88, "Ikot-Ekpene", 3 },
                    { 60, "Numan Nigeria", 2 },
                    { 59, "Mubi South", 2 },
                    { 58, "Mubi North", 2 },
                    { 57, "Michika", 2 },
                    { 67, "Aba South", 1 },
                    { 68, "Arochukwu", 1 },
                    { 69, "Bende", 1 },
                    { 70, "Ikwuano", 1 },
                    { 71, "Isikwuato", 1 },
                    { 72, "Ohafia", 1 },
                    { 73, "Osisioma Ngwa", 1 },
                    { 74, "Ugwunagbo", 1 },
                    { 45, "Demsa", 2 },
                    { 89, "Ini", 3 },
                    { 46, "Fufore", 2 },
                    { 48, "Gayuk", 2 },
                    { 49, "Girei", 2 },
                    { 50, "Gombi", 2 },
                    { 51, "Guyuk", 2 },
                    { 52, "Jada", 2 },
                    { 53, "Lamurde", 2 },
                    { 54, "Madagali", 2 },
                    { 55, "Maiha", 2 },
                    { 56, "Mayo Belwa", 2 },
                    { 47, "Ganye", 2 },
                    { 90, "Itu", 3 },
                    { 91, "Mbo", 3 },
                    { 92, "Mkpat - Enin", 3 },
                    { 117, "Nnewi Nouth", 4 },
                    { 118, "Nnewi South", 4 },
                    { 119, "Ogbaru", 4 },
                    { 120, "Onitsha North", 4 },
                    { 121, "Onitsha South", 4 },
                    { 122, "Orumba North", 4 },
                    { 123, "Orumba South", 4 },
                    { 124, "Oyi", 4 },
                    { 125, "Bauchi", 5 },
                    { 116, "Njikoka", 4 },
                    { 126, "Tafawa Balewa", 5 },
                    { 128, "Darazo", 5 },
                    { 129, "Dass", 5 },
                    { 130, "Toro", 5 },
                    { 131, "Bogoro", 5 },
                    { 132, "Ningi", 5 },
                    { 133, "Warji", 5 },
                    { 134, "Ganjuwa", 5 },
                    { 135, "Kirfi", 5 },
                    { 136, "Alkaleri", 5 },
                    { 127, "Dambam", 5 },
                    { 231, "Ndokwa East", 10 },
                    { 115, "Ihiala", 4 },
                    { 113, "Idemili North", 4 },
                    { 93, "Nsit - Atai", 3 },
                    { 94, "Nsit - Ibom", 3 },
                    { 95, "Nsit - Ubium", 3 },
                    { 96, "Obot - Akara", 3 },
                    { 97, "Okobo", 3 },
                    { 98, "Onna", 3 },
                    { 99, "Oron", 3 },
                    { 100, "Ukanafun", 3 },
                    { 101, "Udung - Uko", 3 },
                    { 114, "Idemili South", 4 },
                    { 102, "Uruan", 3 },
                    { 104, "Uyo Anamm", 3 },
                    { 105, "Aguata", 4 },
                    { 106, "Awka North", 4 },
                    { 107, "Awka South", 4 },
                    { 108, "Anambra East", 4 },
                    { 109, "Anaocha", 4 },
                    { 110, "Ayamelum", 4 },
                    { 111, "Dunukofia", 4 },
                    { 112, "Ekwusigo", 4 },
                    { 103, "Urue - Offong / Oruko", 3 },
                    { 423, "Makoda", 19 },
                    { 232, "Ndokwa West", 10 },
                    { 234, "Oshimili South", 10 },
                    { 356, "Gwaram", 17 },
                    { 357, "Gwiwa", 17 },
                    { 358, "Hadejia", 17 },
                    { 359, "Jahun", 17 },
                    { 360, "Kafin Hausa", 17 },
                    { 361, "Kaugama", 17 },
                    { 362, "Kazaure", 17 },
                    { 363, "Kiri Kasama", 17 },
                    { 364, "Kiyawa", 17 },
                    { 365, "Maigatari", 17 },
                    { 366, "Malam Madori", 17 },
                    { 367, "Miga", 17 },
                    { 368, "Ringim", 17 },
                    { 369, "Roni", 17 },
                    { 370, "Sule Tankarkar", 17 },
                    { 371, "Taura", 17 },
                    { 372, "Yankwashi", 17 },
                    { 373, "Birnin Gwari", 18 },
                    { 374, "Chikun", 18 },
                    { 355, "Guri", 17 },
                    { 354, "Gumel", 17 },
                    { 353, "Garki", 17 },
                    { 352, "Gagarawa", 17 },
                    { 332, "Nkwerre", 16 },
                    { 333, "Nwangele", 16 },
                    { 334, "Obowo", 16 },
                    { 335, "Oguta", 16 },
                    { 336, "Ohaji/Egbema", 16 },
                    { 337, "Okigwe", 16 },
                    { 338, "Onuimo", 16 },
                    { 339, "Orlu", 16 },
                    { 340, "Orsu", 16 },
                    { 375, "Giwa", 18 },
                    { 341, "Oru East", 16 },
                    { 343, "Owerri Municipal", 16 },
                    { 344, "Owerri North", 16 },
                    { 345, "Owerri West", 16 },
                    { 346, "Auyo", 17 },
                    { 347, "Babura", 17 },
                    { 348, "Birniwa", 17 },
                    { 349, "Birnin Kudu", 17 },
                    { 350, "Buji", 17 },
                    { 351, "Dutse", 17 },
                    { 342, "Oru West", 16 },
                    { 331, "Njaba", 16 },
                    { 376, "Igabi", 18 },
                    { 378, "Jaba", 18 },
                    { 403, "Dawakin Kudu", 19 },
                    { 404, "Dawakin Tofa", 19 },
                    { 405, "Doguwa", 19 },
                    { 406, "Fagge", 19 },
                    { 407, "Gabasawa", 19 },
                    { 408, "Garko", 19 },
                    { 409, "Garun Mallam", 19 },
                    { 410, "Gaya", 19 },
                    { 411, "Gezawa", 19 },
                    { 412, "Gwale", 19 },
                    { 413, "GWarzo", 19 },
                    { 414, "Kabo", 19 },
                    { 415, "Kano Municipal", 19 },
                    { 416, "Karaye", 19 },
                    { 417, "Kibiya", 19 },
                    { 418, "Kiru", 19 },
                    { 419, "Kumbotso", 19 },
                    { 420, "Kunchi", 19 },
                    { 421, "Kura", 19 },
                    { 402, "Dambatta", 19 },
                    { 401, "Dala", 19 },
                    { 400, "Bunkure", 19 },
                    { 399, "Bichi", 19 },
                    { 379, "Jema'a", 18 },
                    { 380, "Kachia", 18 },
                    { 381, "Kaduna North", 18 },
                    { 382, "Kaduna North", 18 },
                    { 383, "Kagarko", 18 },
                    { 384, "Kajuru", 18 },
                    { 385, "Kaura", 18 },
                    { 386, "Kauru", 18 },
                    { 387, "kubau", 18 },
                    { 377, "Ikara", 18 },
                    { 388, "Kudan", 18 },
                    { 390, "Makarfi", 18 },
                    { 391, "Sabon Gari", 18 },
                    { 392, "Sanga", 18 },
                    { 393, "Soba", 18 },
                    { 394, "Zangon Kataf", 18 },
                    { 395, "Zaria", 18 },
                    { 396, "Albasu", 19 },
                    { 397, "Bagwai", 19 },
                    { 398, "Bebeji", 19 },
                    { 389, "Lere", 18 },
                    { 330, "Ngor Okpala", 16 },
                    { 329, "Mbaitoli", 16 },
                    { 328, "Isu", 16 },
                    { 275, "Esan Central", 12 },
                    { 276, "Esan North-East", 12 },
                    { 277, "Esan South-East", 12 },
                    { 278, "Esan West", 12 },
                    { 279, "Etsako Central", 12 },
                    { 280, "Etsako East", 12 },
                    { 281, "Etsako West", 12 },
                    { 282, "Igueben", 12 },
                    { 283, "Ikpoba-Okha", 12 },
                    { 284, "Oredo", 12 },
                    { 285, "Orhionmwon", 12 },
                    { 286, "Ovia North-East", 12 },
                    { 287, "Ovia South-East", 12 },
                    { 288, "Owan East", 12 },
                    { 289, "Owan West", 12 },
                    { 290, "Uhunmwonde", 12 },
                    { 291, "Ado-Ekiti", 13 },
                    { 292, "Ikere", 13 },
                    { 293, "Oye", 13 },
                    { 274, "Egor", 12 },
                    { 273, "Akoko-Edo", 12 },
                    { 256, "Onicha", 11 },
                    { 255, "Ohaukwu", 11 },
                    { 235, "Ukwuani", 10 },
                    { 236, "Bomadi", 10 },
                    { 237, "Burutu", 10 },
                    { 238, "Isoko North", 10 },
                    { 239, "Isoko South", 10 },
                    { 240, "Patani", 10 },
                    { 241, "Warri North", 10 },
                    { 242, "Warri South", 10 },
                    { 243, "Warri South West", 10 },
                    { 294, "AiyeKire", 13 },
                    { 244, "Abakaliki", 11 },
                    { 246, "Afikpo South", 11 },
                    { 247, "Ebonyi", 11 },
                    { 248, "Ezza North", 11 },
                    { 249, "Ezza South", 11 },
                    { 250, "Ikwo", 11 },
                    { 251, "Ishielu", 11 },
                    { 252, "Ivo", 11 },
                    { 253, "Izzi", 11 },
                    { 254, "Ohaozara", 11 },
                    { 245, "Afikpo North", 11 },
                    { 295, "Efon", 13 },
                    { 296, "Ekiti East", 13 },
                    { 297, "Ekiti South-West", 13 },
                    { 272, "Uzo-Uwani", 14 },
                    { 307, "Akko", 15 },
                    { 308, "Balanga", 15 },
                    { 309, "Biliri", 15 },
                    { 310, "Dukku", 15 },
                    { 311, "Funakaye", 15 },
                    { 312, "Gombe", 15 },
                    { 313, "Kaltungo", 15 },
                    { 314, "Kwami", 15 },
                    { 271, "Udi", 14 },
                    { 315, "Nafada", 15 },
                    { 317, "Yamaltu/Deba", 15 },
                    { 318, "Aboh Mbaise", 16 },
                    { 320, "Ehime Mbano", 16 },
                    { 321, "Ezinihitte Mbaise", 16 },
                    { 322, "Ideato North", 16 },
                    { 323, "Ideato South", 16 },
                    { 324, "Ihitte/Uboma", 16 },
                    { 325, "Ikeduru", 16 },
                    { 326, "Isiala Mbano", 16 },
                    { 316, "Shongom", 15 },
                    { 233, "Oshimili North", 10 },
                    { 270, "Udenu", 14 },
                    { 268, "Nsukka", 14 },
                    { 298, "Ekiti West", 13 },
                    { 299, "Emure", 13 },
                    { 300, "Ido-Osi", 13 },
                    { 301, "Ijero", 13 },
                    { 302, "Ikole", 13 },
                    { 303, "Ilejemeje", 13 },
                    { 304, "Irepodun/Ifelodun", 13 },
                    { 305, "Ise/Orun", 13 },
                    { 306, "Moba", 13 },
                    { 269, "Oji River", 14 },
                    { 257, "Awgu", 14 },
                    { 259, "Enugu North", 14 },
                    { 260, "Enugu North", 14 },
                    { 261, "Ezeagu", 14 },
                    { 262, "Igbo Etiti", 14 },
                    { 263, "Igbo Eze North", 14 },
                    { 264, "Igbo Eze South", 14 },
                    { 265, "Isi Uzo", 14 },
                    { 266, "Nkanu East", 14 },
                    { 267, "Nkanu West", 14 },
                    { 258, "Enugu East", 14 },
                    { 44, "Kwali", 37 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoundItemClaims_ApplicationUserId",
                table: "FoundItemClaims",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundItemClaims_FoundItemId",
                table: "FoundItemClaims",
                column: "FoundItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundItemClaims_ImageId",
                table: "FoundItemClaims",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundItems_ApplicationUserId",
                table: "FoundItems",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundItems_ImageId",
                table: "FoundItems",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundItems_LocalGovernmentId",
                table: "FoundItems",
                column: "LocalGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalGovernments_StateId",
                table: "LocalGovernments",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_LostItemClaims_ApplicationUserId",
                table: "LostItemClaims",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LostItemClaims_ImageId",
                table: "LostItemClaims",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_LostItemClaims_LostItemId",
                table: "LostItemClaims",
                column: "LostItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LostItems_ApplicationUserId",
                table: "LostItems",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LostItems_ImageId",
                table: "LostItems",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_LostItems_LocalGovernmentId",
                table: "LostItems",
                column: "LocalGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_FoundItemId",
                table: "Meetings",
                column: "FoundItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LocalGovernmentId",
                table: "Meetings",
                column: "LocalGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LostItemId",
                table: "Meetings",
                column: "LostItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FoundItemClaims");

            migrationBuilder.DropTable(
                name: "LostItemClaims");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FoundItems");

            migrationBuilder.DropTable(
                name: "LostItems");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "LocalGovernments");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
