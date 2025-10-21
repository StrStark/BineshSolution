using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManager.Migrations.Inventory
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Account_AccountId",
                table: "Inventories");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Account_AccountId",
                table: "Inventories",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Account_AccountId",
                table: "Inventories");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Account_AccountId",
                table: "Inventories",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
