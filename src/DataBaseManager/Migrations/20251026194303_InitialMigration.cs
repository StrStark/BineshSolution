using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    GroupType = table.Column<int>(type: "integer", nullable: false),
                    GroupTypeDesc = table.Column<int>(type: "integer", nullable: false),
                    IsLayerOne = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    SumDebit = table.Column<long>(type: "bigint", nullable: false),
                    SumCredit = table.Column<long>(type: "bigint", nullable: false),
                    Debit = table.Column<long>(type: "bigint", nullable: false),
                    Credit = table.Column<long>(type: "bigint", nullable: false),
                    inflection = table.Column<int>(type: "integer", nullable: false),
                    Article = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DocDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArticleDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    OperationName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    chequeCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SeriesNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Fee = table.Column<long>(type: "bigint", nullable: false),
                    Receipt = table.Column<long>(type: "bigint", nullable: false),
                    Voucher = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Country = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CityRegion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Mahale = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
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
                        name: "FK_Inventories_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Family = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
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
                    ColorPalette = table.Column<string>(type: "text", nullable: true),
                    Density = table.Column<string>(type: "text", nullable: true),
                    ColorCount = table.Column<int>(type: "integer", nullable: false),
                    genus = table.Column<string>(type: "text", nullable: true),
                    Grade = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    BorderColor = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<string>(type: "text", nullable: true),
                    Shoulder = table.Column<string>(type: "text", nullable: true),
                    WeavePattern = table.Column<string>(type: "text", nullable: true),
                    DeviceNumber = table.Column<string>(type: "text", nullable: true),
                    Buyer = table.Column<string>(type: "text", nullable: true),
                    DesignCode = table.Column<string>(type: "text", nullable: true),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    DesignName = table.Column<string>(type: "text", nullable: true),
                    WeaveType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carpets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carpets_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                    Type = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    PackageType = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Serial = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    DeviceUsage = table.Column<string>(type: "text", nullable: true),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    Extra1 = table.Column<string>(type: "text", nullable: true),
                    Extra2 = table.Column<string>(type: "text", nullable: true),
                    Extra3 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawMaterials_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Name = table.Column<string>(type: "text", nullable: true),
                    WeaveType = table.Column<string>(type: "text", nullable: true),
                    Design = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<string>(type: "text", nullable: true),
                    Width = table.Column<string>(type: "text", nullable: true),
                    Buyer = table.Column<string>(type: "text", nullable: true),
                    DesignCode = table.Column<string>(type: "text", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: true),
                    PaymentReliability = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Request = table.Column<bool>(type: "boolean", nullable: false),
                    invoice = table.Column<bool>(type: "boolean", nullable: false),
                    DocNumber = table.Column<int>(type: "integer", nullable: false),
                    CounterpartyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CounterpartyId",
                        column: x => x.CounterpartyId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceId = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Incoming = table.Column<float>(type: "real", nullable: false),
                    Outgoing = table.Column<float>(type: "real", nullable: false),
                    RequestNumber = table.Column<int>(type: "integer", nullable: false),
                    DeliveredQuantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Prices_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Name",
                table: "Accounts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentId",
                table: "Accounts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Carpets_InventoryId",
                table: "Carpets",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PersonId",
                table: "Customers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_AccountId",
                table: "Inventories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CounterpartyId",
                table: "Invoices",
                column: "CounterpartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_RegionId",
                table: "Persons",
                column: "RegionId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Sales_InvoiceId",
                table: "Sales",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PriceId",
                table: "Sales",
                column: "PriceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");
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
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
