using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    pkid = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fullName = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    mail = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.pkid);
                });

            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandName = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    SubCategory = table.Column<string>(type: "text", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedUserId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_brands_users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brands_users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mall-infos",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MallName = table.Column<string>(type: "text", nullable: false),
                    CountryCode = table.Column<string>(type: "text", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: false),
                    CityCode = table.Column<string>(type: "text", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: false),
                    TownCode = table.Column<string>(type: "text", nullable: false),
                    TownName = table.Column<string>(type: "text", nullable: false),
                    LeasableArea = table.Column<int>(type: "integer", nullable: false),
                    VehicleCapacity = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedUserId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mall-infos", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_mall-infos_users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mall-infos_users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MallId = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    MallInfoId = table.Column<int>(type: "integer", nullable: false),
                    MallInfo = table.Column<string>(type: "text", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedUserId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_stores_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stores_mall-infos_MallInfoId",
                        column: x => x.MallInfoId,
                        principalTable: "mall-infos",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stores_users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stores_users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "snapshots",
                columns: table => new
                {
                    PkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    SnapshotDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerCount = table.Column<int>(type: "integer", nullable: false),
                    CustomerInSalesCount = table.Column<int>(type: "integer", nullable: false),
                    WorkerCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedUserId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_snapshots", x => x.PkId);
                    table.ForeignKey(
                        name: "FK_snapshots_stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "stores",
                        principalColumn: "PkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_snapshots_users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_snapshots_users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "users",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_brands_CreatedUserId",
                table: "brands",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_brands_ModifiedUserId",
                table: "brands",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_mall-infos_CreatedUserId",
                table: "mall-infos",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_mall-infos_ModifiedUserId",
                table: "mall-infos",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_snapshots_CreatedUserId",
                table: "snapshots",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_snapshots_ModifiedUserId",
                table: "snapshots",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_snapshots_StoreId",
                table: "snapshots",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_stores_BrandId",
                table: "stores",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_stores_CreatedUserId",
                table: "stores",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_stores_MallInfoId",
                table: "stores",
                column: "MallInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_stores_ModifiedUserId",
                table: "stores",
                column: "ModifiedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "snapshots");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "mall-infos");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
