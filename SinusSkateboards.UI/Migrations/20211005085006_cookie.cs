using Microsoft.EntityFrameworkCore.Migrations;

namespace SinusSkateboards.UI.Migrations
{
    public partial class cookie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Storlek",
                table: "KöptProdukt",
                newName: "Antal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Antal",
                table: "KöptProdukt",
                newName: "Storlek");
        }
    }
}
