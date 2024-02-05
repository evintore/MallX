using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class storeRelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "MallId",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "MallInfo",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "MallName",
                table: "stores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FloorNumber",
                table: "stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MallId",
                table: "stores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MallInfo",
                table: "stores",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MallName",
                table: "stores",
                type: "text",
                nullable: true);
        }
    }
}
