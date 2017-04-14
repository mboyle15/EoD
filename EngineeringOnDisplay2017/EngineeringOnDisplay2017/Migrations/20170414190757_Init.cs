using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EngineeringOnDisplay2017.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingRecords",
                columns: table => new
                {
                    BuildingRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Acronym = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AddressLineOne = table.Column<string>(nullable: true),
                    AddressLineTwo = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(maxLength: 2, nullable: true),
                    Zip = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingRecords", x => x.BuildingRecordId);
                });

            migrationBuilder.CreateTable(
                name: "ElectricalRecords",
                columns: table => new
                {
                    ElectricalRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordedDateTime = table.Column<DateTime>(nullable: false),
                    BuildingRecordId = table.Column<int>(nullable: true),
                    Demand = table.Column<float>(nullable: false),
                    Usage = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricalRecords", x => x.ElectricalRecordId);
                    table.ForeignKey(
                        name: "FK_ElectricalRecords_BuildingRecords_BuildingRecordId",
                        column: x => x.BuildingRecordId,
                        principalTable: "BuildingRecords",
                        principalColumn: "BuildingRecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NaturalGasRecords",
                columns: table => new
                {
                    NaturalGasRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordedDateTime = table.Column<DateTime>(nullable: false),
                    Usage = table.Column<float>(nullable: false),
                    BuildingRecordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalGasRecords", x => x.NaturalGasRecordId);
                    table.ForeignKey(
                        name: "FK_NaturalGasRecords_BuildingRecords_BuildingRecordId",
                        column: x => x.BuildingRecordId,
                        principalTable: "BuildingRecords",
                        principalColumn: "BuildingRecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OutsideTempRecords",
                columns: table => new
                {
                    OutsideTempRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordedDateTime = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<float>(nullable: false),
                    BuildingRecordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutsideTempRecords", x => x.OutsideTempRecordId);
                    table.ForeignKey(
                        name: "FK_OutsideTempRecords_BuildingRecords_BuildingRecordId",
                        column: x => x.BuildingRecordId,
                        principalTable: "BuildingRecords",
                        principalColumn: "BuildingRecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WaterRecords",
                columns: table => new
                {
                    WaterRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordedDateTime = table.Column<DateTime>(nullable: false),
                    Usage = table.Column<float>(nullable: false),
                    BuildingRecordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterRecords", x => x.WaterRecordId);
                    table.ForeignKey(
                        name: "FK_WaterRecords_BuildingRecords_BuildingRecordId",
                        column: x => x.BuildingRecordId,
                        principalTable: "BuildingRecords",
                        principalColumn: "BuildingRecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectricalRecords_BuildingRecordId",
                table: "ElectricalRecords",
                column: "BuildingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricalRecords_RecordedDateTime",
                table: "ElectricalRecords",
                column: "RecordedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_NaturalGasRecords_BuildingRecordId",
                table: "NaturalGasRecords",
                column: "BuildingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_NaturalGasRecords_RecordedDateTime",
                table: "NaturalGasRecords",
                column: "RecordedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutsideTempRecords_BuildingRecordId",
                table: "OutsideTempRecords",
                column: "BuildingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_OutsideTempRecords_RecordedDateTime",
                table: "OutsideTempRecords",
                column: "RecordedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_WaterRecords_BuildingRecordId",
                table: "WaterRecords",
                column: "BuildingRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_WaterRecords_RecordedDateTime",
                table: "WaterRecords",
                column: "RecordedDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricalRecords");

            migrationBuilder.DropTable(
                name: "NaturalGasRecords");

            migrationBuilder.DropTable(
                name: "OutsideTempRecords");

            migrationBuilder.DropTable(
                name: "WaterRecords");

            migrationBuilder.DropTable(
                name: "BuildingRecords");
        }
    }
}
