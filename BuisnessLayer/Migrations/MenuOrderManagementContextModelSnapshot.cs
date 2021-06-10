﻿// <auto-generated />
using System;
using BuisnessLayer.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuisnessLayer.Migrations
{
    [DbContext(typeof(MenuOrderManagementContext))]
    partial class MenuOrderManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BuisnessLayer.DBModels.TblMenu", b =>
                {
                    b.Property<long>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("MenuItem")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("MenuTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("OfferPrice")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("MenuId")
                        .HasName("PK__tblMenu__C99ED2307583CC6C");

                    b.HasIndex("MenuTypeId");

                    b.HasIndex("VendorId");

                    b.ToTable("tblMenu");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.TblMenuType", b =>
                {
                    b.Property<int>("MenuTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("MenuTypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.HasKey("MenuTypeId")
                        .HasName("PK__tblMenuT__8E7B2D6AAD34CA44");

                    b.ToTable("tblMenuType");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.TblVendorList", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("VendorDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("VendorImgLink")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("VendorName")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)");

                    b.Property<int?>("VendorRating")
                        .HasColumnType("int");

                    b.HasKey("VendorId")
                        .HasName("PK__tblVendo__FC8618F3EE5C5634");

                    b.ToTable("tblVendorList");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblLog", b =>
                {
                    b.Property<long>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ControllerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogId");

                    b.ToTable("tblLog");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblPaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PaymentDescription")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PaymentType")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PaymentTypeId");

                    b.ToTable("tblPaymentType");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblRole", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Rolename")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleId");

                    b.ToTable("tblRole");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblUser", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CartAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PictureLocation")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<long>("Points")
                        .HasColumnType("bigint");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("tblUser");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblUserOrder", b =>
                {
                    b.Property<long>("UserOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<long>("MenuId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("UserOrderId");

                    b.HasIndex("MenuId");

                    b.HasIndex("UserId");

                    b.HasIndex("VendorId");

                    b.ToTable("tblUserOrder");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.TblMenu", b =>
                {
                    b.HasOne("BuisnessLayer.DBModels.TblMenuType", "MenuType")
                        .WithMany("TblMenus")
                        .HasForeignKey("MenuTypeId")
                        .HasConstraintName("FK_tblMenuType_MenuTypeId")
                        .IsRequired();

                    b.HasOne("BuisnessLayer.DBModels.TblVendorList", "Vendor")
                        .WithMany("TblMenus")
                        .HasForeignKey("VendorId")
                        .HasConstraintName("FK_tblMenu_VendorId")
                        .IsRequired();

                    b.Navigation("MenuType");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblUser", b =>
                {
                    b.HasOne("BuisnessLayer.DBModels.tblRole", "tblRole")
                        .WithMany("tblUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tblRole");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblUserOrder", b =>
                {
                    b.HasOne("BuisnessLayer.DBModels.TblMenu", "Menu")
                        .WithMany("UserOrders")
                        .HasForeignKey("MenuId")
                        .IsRequired();

                    b.HasOne("BuisnessLayer.DBModels.tblUser", "User")
                        .WithMany("tblUserOrders")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.HasOne("BuisnessLayer.DBModels.TblVendorList", "VendorList")
                        .WithMany("UserOrders")
                        .HasForeignKey("VendorId")
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("User");

                    b.Navigation("VendorList");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.TblMenu", b =>
                {
                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.TblMenuType", b =>
                {
                    b.Navigation("TblMenus");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.TblVendorList", b =>
                {
                    b.Navigation("TblMenus");

                    b.Navigation("UserOrders");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblRole", b =>
                {
                    b.Navigation("tblUsers");
                });

            modelBuilder.Entity("BuisnessLayer.DBModels.tblUser", b =>
                {
                    b.Navigation("tblUserOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
