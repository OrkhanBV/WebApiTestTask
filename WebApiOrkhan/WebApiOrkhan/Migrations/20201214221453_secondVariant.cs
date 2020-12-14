using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiOrkhan.Migrations
{
    public partial class secondVariant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "catecory_type",
                table: "Material");

            migrationBuilder.RenameColumn(
                name: "material_data",
                table: "Material",
                newName: "material_date");

            migrationBuilder.RenameColumn(
                name: "file_data",
                table: "File",
                newName: "file_date");

            migrationBuilder.AlterColumn<long>(
                name: "size",
                table: "File",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "material_date",
                table: "Material",
                newName: "material_data");

            migrationBuilder.RenameColumn(
                name: "file_date",
                table: "File",
                newName: "file_data");

            migrationBuilder.AddColumn<string>(
                name: "catecory_type",
                table: "Material",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "size",
                table: "File",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
