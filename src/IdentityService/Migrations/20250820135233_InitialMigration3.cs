using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBaseManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Token_AspNetUsers_UserId",
                table: "Token");

            migrationBuilder.DropForeignKey(
                name: "FK_Token_UserSession_Id",
                table: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Token_UserId",
                table: "Token");

            migrationBuilder.AddColumn<Guid>(
                name: "UserSessionId",
                table: "Token",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserSessionId",
                table: "Token",
                column: "UserSessionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_UserSession_UserSessionId",
                table: "Token",
                column: "UserSessionId",
                principalTable: "UserSession",
                principalColumn: "SessionUniqueId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Token_UserSession_UserSessionId",
                table: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Token_UserSessionId",
                table: "Token");

            migrationBuilder.DropColumn(
                name: "UserSessionId",
                table: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Token_AspNetUsers_UserId",
                table: "Token",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_UserSession_Id",
                table: "Token",
                column: "Id",
                principalTable: "UserSession",
                principalColumn: "SessionUniqueId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
