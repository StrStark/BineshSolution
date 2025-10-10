using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManager.Migrations.Sales
{
    /// <inheritdoc />
    public partial class InitialCreate_Sales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carpets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Manufacturer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryDesc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InventoryDesc2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InventoryDescBarcode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryDescLatin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryIsActive = table.Column<bool>(type: "boolean", nullable: false),
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
                    Counterparty = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Manufacturer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryDesc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InventoryDesc2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InventoryDescBarcode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryDescLatin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryIsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RawMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Manufacturer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryDesc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InventoryDesc2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InventoryDescBarcode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryDescLatin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryIsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    PackageType = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Serial = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    DeviceUsage = table.Column<string>(type: "text", nullable: false),
                    ProjectName = table.Column<string>(type: "text", nullable: false),
                    Extra1 = table.Column<string>(type: "text", nullable: false),
                    Extra2 = table.Column<string>(type: "text", nullable: false),
                    Extra3 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Manufacturer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryDesc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InventoryDesc2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    InventoryDescBarcode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryDescLatin = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InventoryIsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WeaveType = table.Column<string>(type: "text", nullable: false),
                    Design = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<string>(type: "text", nullable: false),
                    Width = table.Column<string>(type: "text", nullable: false),
                    Buyer = table.Column<string>(type: "text", nullable: false),
                    DesignCode = table.Column<string>(type: "text", nullable: false),
                    ColorCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rugs", x => x.Id);
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
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
