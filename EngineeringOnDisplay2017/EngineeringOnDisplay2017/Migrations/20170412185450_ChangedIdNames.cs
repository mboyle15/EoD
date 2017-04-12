using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineeringOnDisplay2017.Migrations
{
    public partial class ChangedIdNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WaterRecords",
                newName: "WaterRecordId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OutsideTempRecords",
                newName: "OutsideTempRecordId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "NaturalGasRecords",
                newName: "NaturalGasRecordId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EletricalRecords",
                newName: "ElectricalRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WaterRecordId",
                table: "WaterRecords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OutsideTempRecordId",
                table: "OutsideTempRecords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "NaturalGasRecordId",
                table: "NaturalGasRecords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ElectricalRecordId",
                table: "EletricalRecords",
                newName: "Id");
        }
    }
}
