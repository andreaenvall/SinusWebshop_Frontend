using Microsoft.EntityFrameworkCore.Migrations;

namespace SinusSkateboards.UI.Migrations
{
    public partial class imgpath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "KundModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "KundModel");
        }
    }
}
