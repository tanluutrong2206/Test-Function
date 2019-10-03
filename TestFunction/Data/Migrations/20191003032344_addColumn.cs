using Microsoft.EntityFrameworkCore.Migrations;

namespace TestFunction.Data.Migrations
{
    public partial class addColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Folder",
                table: "TreeDatas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Lazy",
                table: "TreeDatas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Folder",
                table: "TreeDatas");

            migrationBuilder.DropColumn(
                name: "Lazy",
                table: "TreeDatas");
        }
    }
}
