using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MisFinder.Data.Migrations
{
    public partial class first : Migration
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
                    FullName = table.Column<string>(nullable: true)
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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

            migrationBuilder.CreateTable(
                name: "FoundItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameOfFoundItem = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    LocalGovernmentId = table.Column<int>(nullable: false),
                    ExactArea = table.Column<string>(nullable: true),
                    WhereItemWasFound = table.Column<string>(maxLength: 100, nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    DateFound = table.Column<DateTime>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    IsClaimed = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameOfLostItem = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    ExactArea = table.Column<string>(nullable: true),
                    LocalGovernmentId = table.Column<int>(nullable: false),
                    WhereItemWasLost = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    DateMisplaced = table.Column<DateTime>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    IsFound = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FoundItemId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "LostItemClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    LostItemId = table.Column<int>(nullable: false),
                    Sorted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true)
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
                        name: "FK_LostItemClaims_LostItems_LostItemId",
                        column: x => x.LostItemId,
                        principalTable: "LostItems",
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

            migrationBuilder.InsertData(
                table: "LocalGovernments",
                columns: new[] { "Id", "Name", "StateId" },
                values: new object[,]
                {
                    { 66, "Aba North", 1 },
                    { 431, "Takai", 19 },
                    { 432, "Tarauni", 19 },
                    { 433, "Tofa", 19 },
                    { 434, "Tsanyawa", 19 },
                    { 435, "Tudun Wada", 19 },
                    { 436, "Ungogo", 19 },
                    { 430, "Sumaila", 19 },
                    { 437, "Warawa", 19 },
                    { 439, "Bakori", 20 },
                    { 440, "Batagarawa", 20 },
                    { 441, "Batsari", 20 },
                    { 442, "Baure", 20 },
                    { 443, "Bindawa", 20 },
                    { 444, "Charanchi", 20 },
                    { 438, "Wudil", 19 },
                    { 445, "Dan Musa", 20 },
                    { 429, "Shanono", 19 },
                    { 427, "Rimin Gado", 19 },
                    { 413, "GWarzo", 19 },
                    { 414, "Kabo", 19 },
                    { 415, "Kano Municipal", 19 },
                    { 416, "Karaye", 19 },
                    { 417, "Kibiya", 19 },
                    { 418, "Kiru", 19 },
                    { 428, "Rogo", 19 },
                    { 419, "Kumbotso", 19 },
                    { 421, "Kura", 19 },
                    { 422, "Madobi", 19 },
                    { 423, "Makoda", 19 },
                    { 424, "Minjibir", 19 },
                    { 425, "Nasarawa", 19 },
                    { 426, "Rano", 19 },
                    { 420, "Kunchi", 19 },
                    { 446, "Dandume", 20 },
                    { 447, "Danja", 20 },
                    { 448, "Daura", 20 },
                    { 468, "Rimi", 20 },
                    { 469, "Sabuwa", 20 },
                    { 470, "Safana", 20 },
                    { 471, "Sandanu", 20 },
                    { 472, "Zango", 20 },
                    { 473, "Aliero", 21 },
                    { 467, "Musawa", 20 },
                    { 474, "Arewa Dandi", 21 },
                    { 476, "Augie", 21 },
                    { 477, "Bagudo", 21 },
                    { 478, "Birnin Kebbi", 21 },
                    { 480, "Bunza", 21 },
                    { 481, "Dandi", 21 },
                    { 482, "Fakai", 21 },
                    { 475, "Argungu", 21 },
                    { 466, "Matazu", 20 },
                    { 465, "Mashi", 20 },
                    { 464, "Mani", 20 },
                    { 449, "Dutsi", 20 },
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
                    { 412, "Gwale", 19 },
                    { 483, "Gwandu", 21 },
                    { 411, "Gezawa", 19 },
                    { 409, "Garun Mallam", 19 },
                    { 358, "Hadejia", 17 },
                    { 359, "Jahun", 17 },
                    { 360, "Kafin Hausa", 17 },
                    { 361, "Kaugama", 17 },
                    { 362, "Kazaure", 17 },
                    { 363, "Kiri Kasama", 17 },
                    { 357, "Gwiwa", 17 },
                    { 364, "Kiyawa", 17 },
                    { 366, "Malam Madori", 17 },
                    { 367, "Miga", 17 },
                    { 368, "Ringim", 17 },
                    { 369, "Roni", 17 },
                    { 370, "Sule Tankarkar", 17 },
                    { 371, "Taura", 17 },
                    { 365, "Maigatari", 17 },
                    { 372, "Yankwashi", 17 },
                    { 356, "Gwaram", 17 },
                    { 354, "Gumel", 17 },
                    { 340, "Orsu", 16 },
                    { 341, "Oru East", 16 },
                    { 342, "Oru West", 16 },
                    { 343, "Owerri Municipal", 16 },
                    { 344, "Owerri North", 16 },
                    { 345, "Owerri West", 16 },
                    { 355, "Guri", 17 },
                    { 346, "Auyo", 17 },
                    { 348, "Birniwa", 17 },
                    { 349, "Birnin Kudu", 17 },
                    { 350, "Buji", 17 },
                    { 351, "Dutse", 17 },
                    { 352, "Gagarawa", 17 },
                    { 353, "Garki", 17 },
                    { 347, "Babura", 17 },
                    { 373, "Birnin Gwari", 18 },
                    { 374, "Chikun", 18 },
                    { 375, "Giwa", 18 },
                    { 395, "Zaria", 18 },
                    { 396, "Albasu", 19 },
                    { 397, "Bagwai", 19 },
                    { 398, "Bebeji", 19 },
                    { 399, "Bichi", 19 },
                    { 400, "Bunkure", 19 },
                    { 394, "Zangon Kataf", 18 },
                    { 401, "Dala", 19 },
                    { 403, "Dawakin Kudu", 19 },
                    { 404, "Dawakin Tofa", 19 },
                    { 405, "Doguwa", 19 },
                    { 406, "Fagge", 19 },
                    { 407, "Gabasawa", 19 },
                    { 408, "Garko", 19 },
                    { 402, "Dambatta", 19 },
                    { 393, "Soba", 18 },
                    { 392, "Sanga", 18 },
                    { 391, "Sabon Gari", 18 },
                    { 376, "Igabi", 18 },
                    { 377, "Ikara", 18 },
                    { 378, "Jaba", 18 },
                    { 379, "Jema'a", 18 },
                    { 380, "Kachia", 18 },
                    { 381, "Kaduna North", 18 },
                    { 382, "Kaduna North", 18 },
                    { 383, "Kagarko", 18 },
                    { 384, "Kajuru", 18 },
                    { 385, "Kaura", 18 },
                    { 386, "Kauru", 18 },
                    { 387, "kubau", 18 },
                    { 388, "Kudan", 18 },
                    { 389, "Lere", 18 },
                    { 390, "Makarfi", 18 },
                    { 410, "Gaya", 19 },
                    { 339, "Orlu", 16 },
                    { 484, "Jega", 21 },
                    { 486, "Koko/ Besse", 21 },
                    { 558, "Magama", 26 },
                    { 559, "Mariga", 26 },
                    { 560, "Mashegu", 26 },
                    { 561, "Mokwa", 26 },
                    { 562, "Munya", 26 },
                    { 563, "Paikoro", 26 },
                    { 557, "Lavun", 26 },
                    { 564, "Rafi", 26 },
                    { 566, "Shiroro", 26 },
                    { 567, "Suleja", 26 },
                    { 568, "Tafa", 26 },
                    { 569, "Wushishi", 26 },
                    { 21, "Abeokuta North", 27 },
                    { 22, "Ado-Odo/Ota", 27 },
                    { 565, "Rijau", 26 },
                    { 23, "Ewekoro", 27 },
                    { 556, "Lapai", 26 },
                    { 554, "Katcha", 26 },
                    { 540, "Nassarawa", 25 },
                    { 541, "Nassarawa-Egbon", 25 },
                    { 542, "Obi", 25 },
                    { 543, "Toto", 25 },
                    { 544, "Wamba", 25 },
                    { 545, "Agaie", 26 },
                    { 555, "Kontagora", 26 },
                    { 546, "Agwara", 26 },
                    { 548, "Borgu", 26 },
                    { 549, "Bosso", 26 },
                    { 550, "Chanchaga", 26 },
                    { 551, "Edati", 26 },
                    { 552, "Gbako", 26 },
                    { 553, "Gurara", 26 },
                    { 547, "Bida", 26 },
                    { 24, "Ifo", 27 },
                    { 25, "Ijebu East", 27 },
                    { 26, "Ijebu North", 27 },
                    { 576, "Ese Odo", 28 },
                    { 577, "Idanre", 28 },
                    { 578, "Ifedore", 28 },
                    { 579, "Ilaje", 28 },
                    { 580, "Ile Oluji/OKeigbo", 28 },
                    { 581, "Irele", 28 },
                    { 575, "Akure South", 28 },
                    { 582, "Odigbo", 28 },
                    { 584, "Ondo West", 28 },
                    { 585, "Ose", 28 },
                    { 586, "Owo", 28 },
                    { 40, "Gwagwalada", 37 },
                    { 41, "Abaji", 37 },
                    { 42, "Abuja Municipal", 37 },
                    { 583, "Okitipupa", 28 },
                    { 574, "Akure North", 28 },
                    { 573, "Akoko South-West", 28 },
                    { 572, "Akoko South-East", 28 },
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
                    { 539, "Lafia", 25 },
                    { 485, "Kalgo", 21 },
                    { 538, "Kokona", 25 },
                    { 536, "Keana", 25 },
                    { 505, "Koton Karfe", 22 },
                    { 506, "Lokoja", 22 },
                    { 507, "Mopa-Muro", 22 },
                    { 508, "Ofu", 22 },
                    { 509, "Ogori/Magongo", 22 },
                    { 510, "Okehi", 22 },
                    { 504, "Kabba/Bunu", 22 },
                    { 511, "Okene", 22 },
                    { 513, "Omala", 22 },
                    { 514, "Yagba East", 22 },
                    { 515, "Yagba West", 22 },
                    { 516, "Asa", 23 },
                    { 517, "Baruten", 23 },
                    { 518, "Edu", 23 },
                    { 512, "Olamaboro", 22 },
                    { 519, "Ekiti", 23 },
                    { 503, "Ijumu", 22 },
                    { 501, "Idah", 22 },
                    { 487, "Maiyama", 21 },
                    { 488, "Ngaski", 21 },
                    { 489, "Sakaba", 21 },
                    { 490, "Shanga", 21 },
                    { 491, "Suru", 21 },
                    { 492, "Danko/Wasagu", 21 },
                    { 502, "Igalamela-Odulu", 22 },
                    { 493, "Yauri", 21 },
                    { 495, "Adavi", 22 },
                    { 496, "Ajaokuta", 22 },
                    { 497, "Ankpa", 22 },
                    { 498, "Bassa", 22 },
                    { 499, "Dekina", 22 },
                    { 500, "Ibaji", 22 },
                    { 494, "Zuru", 21 },
                    { 520, "Ifolodun", 23 },
                    { 521, "Ilorin East", 23 },
                    { 522, "Ilorin South", 23 },
                    { 11, "LagosIsland", 24 },
                    { 12, "Lagos Mainland", 24 },
                    { 13, "Surulere", 24 },
                    { 14, "Ojo", 24 },
                    { 15, "Ajeromi-Ifelodun", 24 },
                    { 16, "Amuwo-Odofin", 24 },
                    { 10, "Eti-Osa", 24 },
                    { 17, "Badagry", 24 },
                    { 19, "Ibeju-Lekki", 24 },
                    { 20, "Epe", 24 },
                    { 532, "Akwanga", 25 },
                    { 533, "Awe", 25 },
                    { 534, "Doma", 25 },
                    { 535, "Karu", 25 },
                    { 18, "Ikorodu", 24 },
                    { 9, "Shomolu", 24 },
                    { 8, "Oshodi-Isolo", 24 },
                    { 7, "Mushin", 24 },
                    { 523, "Ilorin West", 23 },
                    { 524, "Irepodun", 23 },
                    { 525, "Isin", 23 },
                    { 526, "Kaiama", 23 },
                    { 527, "Moro", 23 },
                    { 528, "Offa", 23 },
                    { 529, "Oke Ero", 23 },
                    { 530, "Oyun", 23 },
                    { 531, "Patigi", 23 },
                    { 1, "Agege", 24 },
                    { 2, "Alimosho", 24 },
                    { 3, "Apapa", 24 },
                    { 4, "Ifako-Ijaye", 24 },
                    { 5, "Ikeja", 24 },
                    { 6, "Kosofe", 24 },
                    { 537, "Keffi", 25 },
                    { 43, "Bwari", 37 },
                    { 338, "Onuimo", 16 },
                    { 336, "Ohaji/Egbema", 16 },
                    { 137, "Misau", 5 },
                    { 138, "Giade", 5 },
                    { 139, "Shira", 5 },
                    { 140, "Jama'are", 5 },
                    { 141, "Katagum", 5 },
                    { 142, "Itas/Gadau", 5 },
                    { 136, "Alkaleri", 5 },
                    { 143, "Zaki", 5 },
                    { 145, "Brass", 6 },
                    { 146, "Ekeremor", 6 },
                    { 147, "Kolokuma/Opokuma", 6 },
                    { 148, "Nembe", 6 },
                    { 149, "Ogbia", 6 },
                    { 150, "Sagbama", 6 },
                    { 144, "Gamawa", 5 },
                    { 151, "Southern Ijaw", 6 },
                    { 135, "Kirfi", 5 },
                    { 133, "Warji", 5 },
                    { 119, "Ogbaru", 4 },
                    { 120, "Onitsha North", 4 },
                    { 121, "Onitsha South", 4 },
                    { 122, "Orumba North", 4 },
                    { 123, "Orumba South", 4 },
                    { 124, "Oyi", 4 },
                    { 134, "Ganjuwa", 5 },
                    { 125, "Bauchi", 5 },
                    { 127, "Dambam", 5 },
                    { 128, "Darazo", 5 },
                    { 129, "Dass", 5 },
                    { 130, "Toro", 5 },
                    { 131, "Bogoro", 5 },
                    { 132, "Ningi", 5 },
                    { 126, "Tafawa Balewa", 5 },
                    { 319, "Yenagoa", 6 },
                    { 152, "Ado", 7 },
                    { 153, "Agatu", 7 },
                    { 173, "Ushongo", 7 },
                    { 174, "Vandeikya", 7 },
                    { 175, "Maigugri", 8 },
                    { 176, "Ngala", 8 },
                    { 177, "Kala/Balge", 8 },
                    { 178, "Mafa", 8 },
                    { 172, "Ukum", 7 },
                    { 179, "Konduga", 8 },
                    { 181, "Jere", 8 },
                    { 182, "Dikwa", 8 },
                    { 183, "Askira/Uba", 8 },
                    { 184, "Bayo", 8 },
                    { 185, "Biu", 8 },
                    { 186, "Chibok", 8 },
                    { 180, "Bama", 8 },
                    { 171, "Tarka", 7 },
                    { 170, "Otukpo", 7 },
                    { 169, "Okpokwu", 7 },
                    { 154, "Apa", 7 },
                    { 155, "Buruku", 7 },
                    { 156, "Gboko", 7 },
                    { 157, "Guma", 7 },
                    { 158, "Gwer East", 7 },
                    { 159, "Gwer Wast", 7 },
                    { 160, "Katsina Ala", 7 },
                    { 161, "KOnshiha", 7 },
                    { 162, "Kwande", 7 },
                    { 163, "Logo", 7 },
                    { 164, "Makurdi", 7 },
                    { 165, "Obi", 7 },
                    { 166, "Ogbadibo", 7 },
                    { 167, "Ohimmini", 7 },
                    { 168, "Oju", 7 },
                    { 118, "Nnewi South", 4 },
                    { 187, "Damboa", 8 },
                    { 117, "Nnewi Nouth", 4 },
                    { 115, "Ihiala", 4 },
                    { 55, "Maiha", 2 },
                    { 56, "Mayo Belwa", 2 },
                    { 57, "Michika", 2 },
                    { 58, "Mubi North", 2 },
                    { 59, "Mubi South", 2 },
                    { 60, "Numan Nigeria", 2 },
                    { 54, "Madagali", 2 },
                    { 61, "Shelling", 2 },
                    { 63, "Toungo", 2 },
                    { 64, "Yola North", 2 },
                    { 65, "Yola South", 2 },
                    { 75, "Abak", 3 },
                    { 76, "Eastern Obolo", 3 },
                    { 77, "Eket", 3 },
                    { 62, "Song", 2 },
                    { 78, "Esit-Eket", 3 },
                    { 53, "Lamurde", 2 },
                    { 51, "Guyuk", 2 },
                    { 67, "Aba South", 1 },
                    { 68, "Arochukwu", 1 },
                    { 69, "Bende", 1 },
                    { 70, "Ikwuano", 1 },
                    { 71, "Isikwuato", 1 },
                    { 72, "Ohafia", 1 },
                    { 52, "Jada", 2 },
                    { 73, "Osisioma Ngwa", 1 },
                    { 45, "Demsa", 2 },
                    { 46, "Fufore", 2 },
                    { 47, "Ganye", 2 },
                    { 48, "Gayuk", 2 },
                    { 49, "Girei", 2 },
                    { 50, "Gombi", 2 },
                    { 74, "Ugwunagbo", 1 },
                    { 79, "Essien-Ekpo", 3 },
                    { 80, "Etim-Ukpom", 3 },
                    { 81, "Etinan", 3 },
                    { 101, "Udung - Uko", 3 },
                    { 102, "Uruan", 3 },
                    { 103, "Urue - Offong / Oruko", 3 },
                    { 104, "Uyo Anamm", 3 },
                    { 105, "Aguata", 4 },
                    { 106, "Awka North", 4 },
                    { 100, "Ukanafun", 3 },
                    { 107, "Awka South", 4 },
                    { 109, "Anaocha", 4 },
                    { 110, "Ayamelum", 4 },
                    { 111, "Dunukofia", 4 },
                    { 112, "Ekwusigo", 4 },
                    { 113, "Idemili North", 4 },
                    { 114, "Idemili South", 4 },
                    { 108, "Anambra East", 4 },
                    { 99, "Oron", 3 },
                    { 98, "Onna", 3 },
                    { 97, "Okobo", 3 },
                    { 82, "Ibeemo", 3 },
                    { 83, "Ibesikpo - Asutan", 3 },
                    { 84, "Ibiono-Ibom", 3 },
                    { 85, "Ika", 3 },
                    { 86, "Ikono", 3 },
                    { 87, "Ikot-Abasi", 3 },
                    { 88, "Ikot-Ekpene", 3 },
                    { 89, "Ini", 3 },
                    { 90, "Itu", 3 },
                    { 91, "Mbo", 3 },
                    { 92, "Mkpat - Enin", 3 },
                    { 93, "Nsit - Atai", 3 },
                    { 94, "Nsit - Ibom", 3 },
                    { 95, "Nsit - Ubium", 3 },
                    { 96, "Obot - Akara", 3 },
                    { 116, "Njikoka", 4 },
                    { 337, "Okigwe", 16 },
                    { 188, "Gwoza", 8 },
                    { 190, "Kwaya Kusar", 8 },
                    { 299, "Emure", 13 },
                    { 300, "Ido-Osi", 13 },
                    { 301, "Ijero", 13 },
                    { 302, "Ikole", 13 },
                    { 303, "Ilejemeje", 13 },
                    { 304, "Irepodun/Ifelodun", 13 },
                    { 298, "Ekiti West", 13 },
                    { 305, "Ise/Orun", 13 },
                    { 257, "Awgu", 14 },
                    { 258, "Enugu East", 14 },
                    { 259, "Enugu North", 14 },
                    { 260, "Enugu North", 14 },
                    { 261, "Ezeagu", 14 },
                    { 262, "Igbo Etiti", 14 },
                    { 306, "Moba", 13 },
                    { 263, "Igbo Eze North", 14 },
                    { 297, "Ekiti South-West", 13 },
                    { 295, "Efon", 13 },
                    { 281, "Etsako West", 12 },
                    { 282, "Igueben", 12 },
                    { 283, "Ikpoba-Okha", 12 },
                    { 284, "Oredo", 12 },
                    { 285, "Orhionmwon", 12 },
                    { 286, "Ovia North-East", 12 },
                    { 296, "Ekiti East", 13 },
                    { 287, "Ovia South-East", 12 },
                    { 289, "Owan West", 12 },
                    { 290, "Uhunmwonde", 12 },
                    { 291, "Ado-Ekiti", 13 },
                    { 292, "Ikere", 13 },
                    { 293, "Oye", 13 },
                    { 294, "AiyeKire", 13 },
                    { 288, "Owan East", 12 },
                    { 264, "Igbo Eze South", 14 },
                    { 265, "Isi Uzo", 14 },
                    { 266, "Nkanu East", 14 },
                    { 321, "Ezinihitte Mbaise", 16 },
                    { 322, "Ideato North", 16 },
                    { 323, "Ideato South", 16 },
                    { 324, "Ihitte/Uboma", 16 },
                    { 325, "Ikeduru", 16 },
                    { 326, "Isiala Mbano", 16 },
                    { 320, "Ehime Mbano", 16 },
                    { 328, "Isu", 16 },
                    { 330, "Ngor Okpala", 16 },
                    { 331, "Njaba", 16 },
                    { 332, "Nkwerre", 16 },
                    { 333, "Nwangele", 16 },
                    { 334, "Obowo", 16 },
                    { 335, "Oguta", 16 },
                    { 329, "Mbaitoli", 16 },
                    { 318, "Aboh Mbaise", 16 },
                    { 317, "Yamaltu/Deba", 15 },
                    { 316, "Shongom", 15 },
                    { 267, "Nkanu West", 14 },
                    { 268, "Nsukka", 14 },
                    { 269, "Oji River", 14 },
                    { 270, "Udenu", 14 },
                    { 271, "Udi", 14 },
                    { 272, "Uzo-Uwani", 14 },
                    { 307, "Akko", 15 },
                    { 308, "Balanga", 15 },
                    { 309, "Biliri", 15 },
                    { 310, "Dukku", 15 },
                    { 311, "Funakaye", 15 },
                    { 312, "Gombe", 15 },
                    { 313, "Kaltungo", 15 },
                    { 314, "Kwami", 15 },
                    { 315, "Nafada", 15 },
                    { 280, "Etsako East", 12 },
                    { 189, "Hawul", 8 },
                    { 279, "Etsako Central", 12 },
                    { 277, "Esan South-East", 12 },
                    { 209, "Calabar South", 9 },
                    { 210, "Etung", 9 },
                    { 211, "Ikom", 9 },
                    { 212, "Obanliku", 9 },
                    { 213, "Obubra", 9 },
                    { 214, "Obudu", 9 },
                    { 208, "Calar Municipal", 9 },
                    { 215, "Odukpani", 9 },
                    { 217, "Yakuur", 9 },
                    { 218, "Yala", 9 },
                    { 219, "Ethiope East", 10 },
                    { 220, "Ethiope West", 10 },
                    { 221, "Okpe", 10 },
                    { 222, "Sapele", 10 },
                    { 216, "Ogoja", 9 },
                    { 223, "Udu", 10 },
                    { 207, "Boki", 9 },
                    { 205, "Bekwarra", 9 },
                    { 191, "Shani", 8 },
                    { 192, "Abadam", 8 },
                    { 193, "Gubio", 8 },
                    { 194, "Guzamala", 8 },
                    { 195, "Kaga", 8 },
                    { 196, "Maumeri", 8 },
                    { 206, "Biase", 9 },
                    { 197, "Marte", 8 },
                    { 199, "Monguno", 8 },
                    { 200, "Nganzai", 8 },
                    { 201, "Abi", 9 },
                    { 202, "Akamkpa", 9 },
                    { 203, "Akpabuyo", 9 },
                    { 204, "Bakassi", 9 },
                    { 198, "Mobbar", 8 },
                    { 224, "Ughelli North", 10 },
                    { 225, "Uvwie", 10 },
                    { 226, "Aniocha North", 10 },
                    { 247, "Ebonyi", 11 },
                    { 248, "Ezza North", 11 },
                    { 249, "Ezza South", 11 },
                    { 250, "Ikwo", 11 },
                    { 251, "Ishielu", 11 },
                    { 252, "Ivo", 11 },
                    { 246, "Afikpo South", 11 },
                    { 253, "Izzi", 11 },
                    { 255, "Ohaukwu", 11 },
                    { 256, "Onicha", 11 },
                    { 273, "Akoko-Edo", 12 },
                    { 274, "Egor", 12 },
                    { 275, "Esan Central", 12 },
                    { 276, "Esan North-East", 12 },
                    { 254, "Ohaozara", 11 },
                    { 245, "Afikpo North", 11 },
                    { 244, "Abakaliki", 11 },
                    { 243, "Warri South West", 10 },
                    { 227, "Aniocha South", 10 },
                    { 228, "Ika North East", 10 },
                    { 230, "Ika South", 10 },
                    { 231, "Ndokwa East", 10 },
                    { 232, "Ndokwa West", 10 },
                    { 233, "Oshimili North", 10 },
                    { 234, "Oshimili South", 10 },
                    { 235, "Ukwuani", 10 },
                    { 236, "Bomadi", 10 },
                    { 237, "Burutu", 10 },
                    { 238, "Isoko North", 10 },
                    { 239, "Isoko South", 10 },
                    { 240, "Patani", 10 },
                    { 241, "Warri North", 10 },
                    { 242, "Warri South", 10 },
                    { 278, "Esan West", 12 },
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
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FoundItemClaims_ApplicationUserId",
                table: "FoundItemClaims",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoundItemClaims_FoundItemId",
                table: "FoundItemClaims",
                column: "FoundItemId");

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
