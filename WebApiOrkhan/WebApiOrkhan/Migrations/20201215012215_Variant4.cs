using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiOrkhan.Migrations
{
    public partial class Variant4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Categories_CategoryId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_CategoryId",
                table: "Materials");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CategoryId",
                table: "Materials",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Categories_CategoryId",
                table: "Materials",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
