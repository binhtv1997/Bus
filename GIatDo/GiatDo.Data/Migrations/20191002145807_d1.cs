using Microsoft.EntityFrameworkCore.Migrations;

namespace GiatDo.Data.Migrations
{
    public partial class d1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Store",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Slot",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Shipper",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ServiceType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Service",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "OrderService",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Order",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Customer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Admin",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Account",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Slot");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Shipper");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ServiceType");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "OrderService");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Account");
        }
    }
}
