using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GiatDo.Data.Migrations
{
    public partial class aa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Account_AccountId",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_AccountId",
                table: "Store");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Store",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "User_Id",
                table: "Account",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Store_AccountId",
                table: "Store",
                column: "AccountId",
                unique: true,
                filter: "[AccountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Account_AccountId",
                table: "Store",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Account_AccountId",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_AccountId",
                table: "Store");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Store",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "User_Id",
                table: "Account",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Store_AccountId",
                table: "Store",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Account_AccountId",
                table: "Store",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
