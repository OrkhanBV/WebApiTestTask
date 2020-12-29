using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BabaevTask5.Migrations
{
    public partial class migrInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaterialName = table.Column<string>(type: "text", nullable: true),
                    MaterialDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CategoryType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    PathOfFile = table.Column<string>(type: "text", nullable: true),
                    FileDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialVersions_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialVersions_MaterialId",
                table: "MaterialVersions",
                column: "MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialVersions");

            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
