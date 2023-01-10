﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webshoppen2.Models;

#nullable disable

namespace Webshoppen2.Migrations
{
    [DbContext(typeof(webshoppenContext))]
    partial class webshoppenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Webshoppen2.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AmountofUnits")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.City", b =>
                {
                    b.Property<int>("Ld")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ld");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Ld"), 1L, 1);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Ld")
                        .HasName("PK__City__3213A159F4FB77C6");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasIndex(new[] { "Id" }, "Key");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adress")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("OrderHistoryId")
                        .HasColumnType("int");

                    b.Property<int?>("PaymentInfoId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentInfoID");

                    b.Property<int?>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<long?>("SocialSecurityNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.OrderHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<int?>("ShippingInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderHistory", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.PaymentInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CardNumber")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Method")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("PaymentInfo", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool?>("ChosenProduct")
                        .HasColumnType("bit");

                    b.Property<string>("InfoText")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int")
                        .HasColumnName("SupplierID");

                    b.Property<int?>("UnitsInStock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.ShippingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ParcelServiceName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("ShippingInfo", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Supplier", (string)null);
                });

            modelBuilder.Entity("Webshoppen2.Models.Customer", b =>
                {
                    b.HasOne("Webshoppen2.Models.City", "City")
                        .WithMany("Customers")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK_Customer.CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Webshoppen2.Models.Product", b =>
                {
                    b.HasOne("Webshoppen2.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Product.CategoryId");

                    b.HasOne("Webshoppen2.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId")
                        .HasConstraintName("FK_Product.SupplierID");

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Webshoppen2.Models.Supplier", b =>
                {
                    b.HasOne("Webshoppen2.Models.City", "City")
                        .WithMany("Suppliers")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK_Supplier.CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Webshoppen2.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Webshoppen2.Models.City", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("Webshoppen2.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
