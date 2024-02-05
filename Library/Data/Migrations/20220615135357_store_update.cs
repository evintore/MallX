using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class store_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FloorNumber",
                table: "stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MallName",
                table: "stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "stores",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "MallName",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "stores");
        }
    }
}
