using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarDealer.Data.Migrations
{
    public partial class SmallFixInPartModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qunatity",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "Qunatity",
                table: "Parts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
