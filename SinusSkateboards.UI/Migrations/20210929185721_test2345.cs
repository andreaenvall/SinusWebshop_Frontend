using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SinusSkateboards.UI.Migrations
{
    public partial class test2345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datum",
                table: "KöptProdukt");

            migrationBuilder.DropColumn(
                name: "OrderNummer",
                table: "KöptProdukt");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "KundModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OrderNummer",
                table: "KundModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datum",
                table: "KundModel");

            migrationBuilder.DropColumn(
                name: "OrderNummer",
                table: "KundModel");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "KöptProdukt",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OrderNummer",
                table: "KöptProdukt",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
