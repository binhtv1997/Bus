using Microsoft.EntityFrameworkCore.Migrations;

namespace GiatDo.Data.Migrations
{
    public partial class d4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Store",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Store",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Store",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Store");
        }
    }
}
