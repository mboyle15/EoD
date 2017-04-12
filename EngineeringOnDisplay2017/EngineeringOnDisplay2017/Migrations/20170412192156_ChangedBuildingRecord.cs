using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineeringOnDisplay2017.Migrations
{
    public partial class ChangedBuildingRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "BuildingRecords",
                newName: "Acronym");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BuildingRecords",
                newName: "BuildingRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Acronym",
                table: "BuildingRecords",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "BuildingRecordId",
                table: "BuildingRecords",
                newName: "Id");
        }
    }
}
