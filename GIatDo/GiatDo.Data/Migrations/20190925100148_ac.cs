using Microsoft.EntityFrameworkCore.Migrations;

namespace GiatDo.Data.Migrations
{
    public partial class ac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Store",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Store");
        }
    }
}
