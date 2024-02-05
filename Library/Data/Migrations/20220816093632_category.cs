using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "SubCategory",
                table: "brands");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "brands",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "brands",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: true),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedUserId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_categories_users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_categories_users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subcategories",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubategoryId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    SubcategoryName = table.Column<string>(type: "text", nullable: true),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedUserId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subcategories", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_subcategories_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subcategories_users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subcategories_users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_brands_CategoryId",
                table: "brands",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_brands_SubCategoryId",
                table: "brands",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_CreatedUserId",
                table: "categories",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_ModifiedUserId",
                table: "categories",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_subcategories_CategoryId",
                table: "subcategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_subcategories_CreatedUserId",
                table: "subcategories",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_subcategories_ModifiedUserId",
                table: "subcategories",
                column: "ModifiedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_brands_categories_CategoryId",
                table: "brands",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "PkId");

            migrationBuilder.AddForeignKey(
                name: "FK_brands_subcategories_SubCategoryId",
                table: "brands",
                column: "SubCategoryId",
                principalTable: "subcategories",
                principalColumn: "PkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_brands_categories_CategoryId",
                table: "brands");

            migrationBuilder.DropForeignKey(
                name: "FK_brands_subcategories_SubCategoryId",
                table: "brands");

            migrationBuilder.DropTable(
                name: "subcategories");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropIndex(
                name: "IX_brands_CategoryId",
                table: "brands");

            migrationBuilder.DropIndex(
                name: "IX_brands_SubCategoryId",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "brands");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "brands",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubCategory",
                table: "brands",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
