using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineeringOnDisplay2017.Migrations
{
    public partial class DropBuildingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "WaterRecords");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "OutsideTempRecords");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "NaturalGasRecords");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "EletricalRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "WaterRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "OutsideTempRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "NaturalGasRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "EletricalRecords",
                nullable: false,
                defaultValue: 0);
        }
    }
}
