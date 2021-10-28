using Microsoft.EntityFrameworkCore.Migrations;

namespace SinusSkateboards.UI.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "KundModel");

            migrationBuilder.CreateTable(
                name: "KöptProdukt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProduktNamn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Produktkategori = table.Column<int>(type: "int", nullable: false),
                    Produktnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Färg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pris = table.Column<double>(type: "float", nullable: false),
                    Storlek = table.Column<int>(type: "int", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KöptProdukt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KöptProdukt_KundModel_KundId",
                        column: x => x.KundId,
                        principalTable: "KundModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KöptProdukt_KundId",
                table: "KöptProdukt",
                column: "KundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KöptProdukt");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "KundModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
