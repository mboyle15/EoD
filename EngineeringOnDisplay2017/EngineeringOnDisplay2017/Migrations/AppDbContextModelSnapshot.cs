using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EngineeringOnDisplay2017.Models;

namespace EngineeringOnDisplay2017.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.BuildingRecord", b =>
                {
                    b.Property<int>("BuildingRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Acronym")
                        .IsRequired();

                    b.Property<string>("AddressLineOne");

                    b.Property<string>("AddressLineTwo");

                    b.Property<string>("City");

                    b.Property<string>("Name");

                    b.Property<string>("State")
                        .HasMaxLength(2);

                    b.Property<string>("Zip")
                        .HasMaxLength(10);

                    b.HasKey("BuildingRecordId");

                    b.ToTable("BuildingRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.ElectricalRecord", b =>
                {
                    b.Property<int>("ElectricalRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuildingRecordId");

                    b.Property<float>("Demand");

                    b.Property<DateTime>("RecordedDateTime");

                    b.Property<float>("Usage");

                    b.HasKey("ElectricalRecordId");

                    b.HasIndex("BuildingRecordId");

                    b.ToTable("ElectricalRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.NaturalGasRecord", b =>
                {
                    b.Property<int>("NaturalGasRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuildingRecordId");

                    b.Property<DateTime>("RecordedDateTime");

                    b.Property<float>("Usage");

                    b.HasKey("NaturalGasRecordId");

                    b.HasIndex("BuildingRecordId");

                    b.ToTable("NaturalGasRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.OutsideTempRecord", b =>
                {
                    b.Property<int>("OutsideTempRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuildingRecordId");

                    b.Property<DateTime>("RecordedDateTime");

                    b.Property<float>("Temperature");

                    b.HasKey("OutsideTempRecordId");

                    b.HasIndex("BuildingRecordId");

                    b.ToTable("OutsideTempRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.WaterRecord", b =>
                {
                    b.Property<int>("WaterRecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuildingRecordId");

                    b.Property<DateTime>("RecordedDateTime");

                    b.Property<float>("Usage");

                    b.HasKey("WaterRecordId");

                    b.HasIndex("BuildingRecordId");

                    b.ToTable("WaterRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.ElectricalRecord", b =>
                {
                    b.HasOne("EngineeringOnDisplay2017.Models.BuildingRecord", "BuildingRecord")
                        .WithMany()
                        .HasForeignKey("BuildingRecordId");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.NaturalGasRecord", b =>
                {
                    b.HasOne("EngineeringOnDisplay2017.Models.BuildingRecord", "BuildingRecord")
                        .WithMany()
                        .HasForeignKey("BuildingRecordId");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.OutsideTempRecord", b =>
                {
                    b.HasOne("EngineeringOnDisplay2017.Models.BuildingRecord", "BuildingRecord")
                        .WithMany()
                        .HasForeignKey("BuildingRecordId");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.WaterRecord", b =>
                {
                    b.HasOne("EngineeringOnDisplay2017.Models.BuildingRecord", "BuildingRecord")
                        .WithMany()
                        .HasForeignKey("BuildingRecordId");
                });
        }
    }
}
