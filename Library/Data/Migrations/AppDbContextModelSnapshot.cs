﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("pkid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("fullName");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("mail");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("password");

                    b.Property<short>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1);

                    b.HasKey("PkId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Entities.Concrete.Brand", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkId"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ModifiedUserId")
                        .HasColumnType("int");

                    b.Property<int?>("SubCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("PkId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("ModifiedUserId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("brands", (string)null);
                });

            modelBuilder.Entity("Entities.Concrete.Category", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("CategoryName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ModifiedUserId")
                        .HasColumnType("int");

                    b.HasKey("PkId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("ModifiedUserId");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Entities.Concrete.MallInfo", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkId"));

                    b.Property<string>("CityCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("LeasableArea")
                        .HasColumnType("integer");

                    b.Property<string>("MallName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ModifiedUserId")
                        .HasColumnType("int");

                    b.Property<string>("TownCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TownName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VehicleCapacity")
                        .HasColumnType("integer");

                    b.HasKey("PkId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("ModifiedUserId");

                    b.ToTable("mall-infos", (string)null);
                });

            modelBuilder.Entity("Entities.Concrete.Snapshot", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerCount")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerInSalesCount")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ModifiedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SnapshotDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkerCount")
                        .HasColumnType("integer");

                    b.HasKey("PkId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("ModifiedUserId");

                    b.HasIndex("StoreId");

                    b.ToTable("snapshots", (string)null);
                });

            modelBuilder.Entity("Entities.Concrete.Store", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("MallInfoId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ModifiedUserId")
                        .HasColumnType("int");

                    b.Property<string>("StoreName")
                        .HasColumnType("text");

                    b.HasKey("PkId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("MallInfoId");

                    b.HasIndex("ModifiedUserId");

                    b.ToTable("stores", (string)null);
                });

            modelBuilder.Entity("Entities.Concrete.Subcategory", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ModifiedUserId")
                        .HasColumnType("int");

                    b.Property<int>("SubategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("SubcategoryName")
                        .HasColumnType("text");

                    b.HasKey("PkId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("ModifiedUserId");

                    b.ToTable("subcategories", (string)null);
                });

            modelBuilder.Entity("Entities.Concrete.Brand", b =>
                {
                    b.HasOne("Entities.Concrete.Category", "Category")
                        .WithMany("Brands")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Core.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Concrete.Subcategory", "SubCategory")
                        .WithMany("Brands")
                        .HasForeignKey("SubCategoryId");

                    b.Navigation("Category");

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Entities.Concrete.Category", b =>
                {
                    b.HasOne("Core.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("Entities.Concrete.MallInfo", b =>
                {
                    b.HasOne("Core.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("Entities.Concrete.Snapshot", b =>
                {
                    b.HasOne("Core.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Concrete.Store", "Store")
                        .WithMany("Snapshots")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Entities.Concrete.Store", b =>
                {
                    b.HasOne("Entities.Concrete.Brand", "Brand")
                        .WithMany("Stores")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Concrete.MallInfo", "MallInfo")
                        .WithMany("Stores")
                        .HasForeignKey("MallInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("CreatedUser");

                    b.Navigation("MallInfo");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("Entities.Concrete.Subcategory", b =>
                {
                    b.HasOne("Entities.Concrete.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "ModifiedUser")
                        .WithMany()
                        .HasForeignKey("ModifiedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("CreatedUser");

                    b.Navigation("ModifiedUser");
                });

            modelBuilder.Entity("Entities.Concrete.Brand", b =>
                {
                    b.Navigation("Stores");
                });

            modelBuilder.Entity("Entities.Concrete.Category", b =>
                {
                    b.Navigation("Brands");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("Entities.Concrete.MallInfo", b =>
                {
                    b.Navigation("Stores");
                });

            modelBuilder.Entity("Entities.Concrete.Store", b =>
                {
                    b.Navigation("Snapshots");
                });

            modelBuilder.Entity("Entities.Concrete.Subcategory", b =>
                {
                    b.Navigation("Brands");
                });
#pragma warning restore 612, 618
        }
    }
}