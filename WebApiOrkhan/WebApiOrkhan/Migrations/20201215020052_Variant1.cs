using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApiOrkhan.Migrations
{
    public partial class Variant1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMaterial");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Materials");

            migrationBuilder.AddColumn<string>(
                name: "category_type",
                table: "Materials",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "text", nullable: true),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    path_of_file = table.Column<string>(type: "text", nullable: true),
                    file_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    materialid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.id);
                    table.ForeignKey(
                        name: "FK_Files_Materials_materialid",
                        column: x => x.materialid,
                        principalTable: "Materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_materialid",
                table: "Files",
                column: "materialid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropColumn(
                name: "category_type",
                table: "Materials");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Materials",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMaterial",
                columns: table => new
                {
                    Categoryid = table.Column<int>(type: "integer", nullable: false),
                    Materialsid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMaterial", x => new { x.Categoryid, x.Materialsid });
                    table.ForeignKey(
                        name: "FK_CategoryMaterial_Categories_Categoryid",
                        column: x => x.Categoryid,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryMaterial_Materials_Materialsid",
                        column: x => x.Materialsid,
                        principalTable: "Materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMaterial_Materialsid",
                table: "CategoryMaterial",
                column: "Materialsid");
        }
    }
}
