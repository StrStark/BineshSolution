using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManager.Inventory
{
    /// <inheritdoc />
    public partial class DbChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Carpets",
                newName: "genus");

            migrationBuilder.RenameColumn(
                name: "ContractCode",
                table: "Carpets",
                newName: "InventoryDescLatin");

            migrationBuilder.AddColumn<string>(
                name: "InventoryCode",
                table: "Rugs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDesc",
                table: "Rugs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDesc2",
                table: "Rugs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDescBarcode",
                table: "Rugs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDescLatin",
                table: "Rugs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InventoryIsActive",
                table: "Rugs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InventoryCode",
                table: "RawMaterials",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDesc",
                table: "RawMaterials",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDesc2",
                table: "RawMaterials",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDescBarcode",
                table: "RawMaterials",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDescLatin",
                table: "RawMaterials",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InventoryIsActive",
                table: "RawMaterials",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InventoryCode",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDesc",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDesc2",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDescBarcode",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDescLatin",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InventoryIsActive",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Density",
                table: "Carpets",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "DesignCode",
                table: "Carpets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryCode",
                table: "Carpets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDesc",
                table: "Carpets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDesc2",
                table: "Carpets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDescBarcode",
                table: "Carpets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InventoryIsActive",
                table: "Carpets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryCode",
                table: "Rugs");

            migrationBuilder.DropColumn(
                name: "InventoryDesc",
                table: "Rugs");

            migrationBuilder.DropColumn(
                name: "InventoryDesc2",
                table: "Rugs");

            migrationBuilder.DropColumn(
                name: "InventoryDescBarcode",
                table: "Rugs");

            migrationBuilder.DropColumn(
                name: "InventoryDescLatin",
                table: "Rugs");

            migrationBuilder.DropColumn(
                name: "InventoryIsActive",
                table: "Rugs");

            migrationBuilder.DropColumn(
                name: "InventoryCode",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "InventoryDesc",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "InventoryDesc2",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "InventoryDescBarcode",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "InventoryDescLatin",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "InventoryIsActive",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "InventoryCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryDesc",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryDesc2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryDescBarcode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryDescLatin",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryIsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DesignCode",
                table: "Carpets");

            migrationBuilder.DropColumn(
                name: "InventoryCode",
                table: "Carpets");

            migrationBuilder.DropColumn(
                name: "InventoryDesc",
                table: "Carpets");

            migrationBuilder.DropColumn(
                name: "InventoryDesc2",
                table: "Carpets");

            migrationBuilder.DropColumn(
                name: "InventoryDescBarcode",
                table: "Carpets");

            migrationBuilder.DropColumn(
                name: "InventoryIsActive",
                table: "Carpets");

            migrationBuilder.RenameColumn(
                name: "genus",
                table: "Carpets",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "InventoryDescLatin",
                table: "Carpets",
                newName: "ContractCode");

            migrationBuilder.AlterColumn<int>(
                name: "Density",
                table: "Carpets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
