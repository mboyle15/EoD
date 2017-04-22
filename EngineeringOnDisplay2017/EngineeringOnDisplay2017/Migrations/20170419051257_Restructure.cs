using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineeringOnDisplay2017.Migrations
{
    public partial class Restructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usage",
                table: "WaterRecords",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "WaterRecordId",
                table: "WaterRecords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "OutsideTempRecords",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "OutsideTempRecordId",
                table: "OutsideTempRecords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Usage",
                table: "NaturalGasRecords",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "NaturalGasRecordId",
                table: "NaturalGasRecords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Usage",
                table: "ElectricalRecords",
                newName: "Change");

            migrationBuilder.RenameColumn(
                name: "Demand",
                table: "ElectricalRecords",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "ElectricalRecordId",
                table: "ElectricalRecords",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "WaterRecords",
                newName: "Usage");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WaterRecords",
                newName: "WaterRecordId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OutsideTempRecords",
                newName: "Temperature");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OutsideTempRecords",
                newName: "OutsideTempRecordId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "NaturalGasRecords",
                newName: "Usage");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "NaturalGasRecords",
                newName: "NaturalGasRecordId");

            migrationBuilder.RenameColumn(
                name: "Change",
                table: "ElectricalRecords",
                newName: "Usage");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ElectricalRecords",
                newName: "Demand");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ElectricalRecords",
                newName: "ElectricalRecordId");
        }
    }
}
