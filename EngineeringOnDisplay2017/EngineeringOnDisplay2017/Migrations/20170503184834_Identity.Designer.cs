using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EngineeringOnDisplay2017.Models;

namespace EngineeringOnDisplay2017.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170503184834_Identity")]
    partial class Identity
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

            modelBuilder.Entity("EngineeringOnDisplay2017.Models.Slide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullUrl");

                    b.Property<int>("Order");

                    b.Property<string>("ThumbUrl");

                    b.Property<int>("TimeSeconds");

                    b.HasKey("Id");

                    b.ToTable("Slides");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
