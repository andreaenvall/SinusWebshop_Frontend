using Microsoft.EntityFrameworkCore.Migrations;

namespace SinusSkateboards.UI.Migrations
{
    public partial class orderid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestallningar_KundModel_KundId",
                table: "Bestallningar");

            migrationBuilder.DropForeignKey(
                name: "FK_Bestallningar_OrderModel_OrdrarId",
                table: "Bestallningar");

            migrationBuilder.DropForeignKey(
                name: "FK_Produkter_OrderModel_OrderModelId",
                table: "Produkter");

            migrationBuilder.DropIndex(
                name: "IX_Produkter_OrderModelId",
                table: "Produkter");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "Produkter");

            migrationBuilder.RenameColumn(
                name: "KundId",
                table: "Bestallningar",
                newName: "KundID");

            migrationBuilder.RenameIndex(
                name: "IX_Bestallningar_KundId",
                table: "Bestallningar",
                newName: "IX_Bestallningar_KundID");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "KundModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrdrarId",
                table: "Bestallningar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KundID",
                table: "Bestallningar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestallningar_KundModel_KundID",
                table: "Bestallningar",
                column: "KundID",
                principalTable: "KundModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestallningar_OrderModel_OrdrarId",
                table: "Bestallningar",
                column: "OrdrarId",
                principalTable: "OrderModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestallningar_KundModel_KundID",
                table: "Bestallningar");

            migrationBuilder.DropForeignKey(
                name: "FK_Bestallningar_OrderModel_OrdrarId",
                table: "Bestallningar");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "KundModel");

            migrationBuilder.RenameColumn(
                name: "KundID",
                table: "Bestallningar",
                newName: "KundId");

            migrationBuilder.RenameIndex(
                name: "IX_Bestallningar_KundID",
                table: "Bestallningar",
                newName: "IX_Bestallningar_KundId");

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "Produkter",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrdrarId",
                table: "Bestallningar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KundId",
                table: "Bestallningar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Produkter_OrderModelId",
                table: "Produkter",
                column: "OrderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestallningar_KundModel_KundId",
                table: "Bestallningar",
                column: "KundId",
                principalTable: "KundModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bestallningar_OrderModel_OrdrarId",
                table: "Bestallningar",
                column: "OrdrarId",
                principalTable: "OrderModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produkter_OrderModel_OrderModelId",
                table: "Produkter",
                column: "OrderModelId",
                principalTable: "OrderModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
