using Microsoft.EntityFrameworkCore.Migrations;

namespace SinusSkateboards.UI.Migrations
{
    public partial class sluttampen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bestallningar");

            migrationBuilder.DropTable(
                name: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "Antal",
                table: "Produkter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Antal",
                table: "Produkter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bestallningar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundID = table.Column<int>(type: "int", nullable: false),
                    OrderNummer = table.Column<int>(type: "int", nullable: false),
                    OrdrarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestallningar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bestallningar_KundModel_KundID",
                        column: x => x.KundID,
                        principalTable: "KundModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bestallningar_OrderModel_OrdrarId",
                        column: x => x.OrdrarId,
                        principalTable: "OrderModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundId = table.Column<int>(type: "int", nullable: false),
                    ProduktId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProducts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestallningar_KundID",
                table: "Bestallningar",
                column: "KundID");

            migrationBuilder.CreateIndex(
                name: "IX_Bestallningar_OrdrarId",
                table: "Bestallningar",
                column: "OrdrarId");
        }
    }
}
