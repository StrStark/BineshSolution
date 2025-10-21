using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManager.Migrations.Inventory
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    GroupType = table.Column<int>(type: "integer", nullable: false),
                    GroupTypeDesc = table.Column<int>(type: "integer", nullable: false),
                    IsLayerOne = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    SumDebit = table.Column<long>(type: "bigint", nullable: false),
                    SumCredit = table.Column<long>(type: "bigint", nullable: false),
                    Debit = table.Column<long>(type: "bigint", nullable: false),
                    Credit = table.Column<long>(type: "bigint", nullable: false),
                    inflection = table.Column<int>(type: "integer", nullable: false),
                    Article = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DocDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArticleDescription = table.Column<string>(type: "text", nullable: true),
                    OperationName = table.Column<string>(type: "text", nullable: true),
                    chequeCode = table.Column<string>(type: "text", nullable: true),
                    SeriesNumber = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Account_Account_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Account",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Manager = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    BarcodeEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    Identifiable = table.Column<bool>(type: "boolean", nullable: false),
                    IsMonetary = table.Column<bool>(type: "boolean", nullable: false),
                    AllowNegativeStock = table.Column<bool>(type: "boolean", nullable: false),
                    UseAverageOrFIFOFromYearStart = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveInReceiptRegistration = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveInReports = table.Column<bool>(type: "boolean", nullable: false),
                    LimitByAccountAndDocumentType = table.Column<bool>(type: "boolean", nullable: false),
                    LimitByMarketer = table.Column<bool>(type: "boolean", nullable: false),
                    AffectStockCalculation = table.Column<bool>(type: "boolean", nullable: false),
                    AffectProductionRequestBySalesOrder = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carpets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Manufacturer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductDesc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductDesc2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductDescBarcode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductDescLatin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductIsActive = table.Column<bool>(type: "boolean", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnteryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    SellingPrice = table.Column<float>(type: "real", nullable: false),
                    ColorPalette = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Density = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ColorCount = table.Column<int>(type: "integer", nullable: false),
                    genus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Grade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BorderColor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Size = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Shoulder = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WeavePattern = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DeviceNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Buyer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DesignCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProjectName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DesignName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    WeaveType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carpets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carpets_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Manufacturer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductDesc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductDesc2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductDescBarcode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductDescLatin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductIsActive = table.Column<bool>(type: "boolean", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnteryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    SellingPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RawMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Manufacturer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductDesc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductDesc2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductDescBarcode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductDescLatin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductIsActive = table.Column<bool>(type: "boolean", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnteryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    SellingPrice = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PackageType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Serial = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeviceUsage = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProjectName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Extra1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Extra2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Extra3 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawMaterials_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Manufacturer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductDesc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductDesc2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProductDescBarcode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductDescLatin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductIsActive = table.Column<bool>(type: "boolean", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnteryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: false),
                    SellingPrice = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    WeaveType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Design = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Size = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Width = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Buyer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DesignCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ColorCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rugs_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ParentId",
                table: "Account",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Carpets_InventoryId",
                table: "Carpets",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_AccountId",
                table: "Inventories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryId",
                table: "Products",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterials_InventoryId",
                table: "RawMaterials",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rugs_InventoryId",
                table: "Rugs",
                column: "InventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carpets");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RawMaterials");

            migrationBuilder.DropTable(
                name: "Rugs");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
