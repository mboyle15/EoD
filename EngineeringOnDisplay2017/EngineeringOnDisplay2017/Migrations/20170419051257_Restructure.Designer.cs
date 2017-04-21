using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EngineeringOnDisplay2017.Models;

namespace EngineeringOnDisplay2017.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170419051257_Restructure")]
    partial class Restructure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<int?>("BuildingRecordId");

                    b.Property<float>("Change");

                    b.Property<DateTime>("RecordedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("BuildingRecordId");

                    b.ToTable("ElectricalRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.NaturalGasRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<int?>("BuildingRecordId");

                    b.Property<DateTime>("RecordedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("BuildingRecordId");

                    b.ToTable("NaturalGasRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.OutsideTempRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<int?>("BuildingRecordId");

                    b.Property<DateTime>("RecordedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("BuildingRecordId");

                    b.ToTable("OutsideTempRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.WaterRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<int?>("BuildingRecordId");

                    b.Property<DateTime>("RecordedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("BuildingRecordId");

                    b.ToTable("WaterRecords");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.ElectricalRecord", b =>
                {
                    b.HasOne("EngineeringOnDisplay2017.Models.BuildingRecord", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingRecordId");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.NaturalGasRecord", b =>
                {
                    b.HasOne("EngineeringOnDisplay2017.Models.BuildingRecord", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingRecordId");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.OutsideTempRecord", b =>
                {
                    b.HasOne("EngineeringOnDisplay2017.Models.BuildingRecord", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingRecordId");
                });

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.WaterRecord", b =>
                {
                    b.HasOne("EngineeringOnDisplay2017.Models.BuildingRecord", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingRecordId");
                });
        }
    }
}
