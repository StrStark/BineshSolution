using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManager.Migrations.Accounting
{
    /// <inheritdoc />
    public partial class InitialCreate_Accounting : Migration
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
                    Desc = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
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
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
