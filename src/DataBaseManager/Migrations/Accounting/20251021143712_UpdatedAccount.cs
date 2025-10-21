using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManager.Migrations.Accounting
{
    /// <inheritdoc />
    public partial class UpdatedAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Accounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Accounts",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
