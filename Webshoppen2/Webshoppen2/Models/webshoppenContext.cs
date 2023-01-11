using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Webshoppen2.Models
{
    public partial class webshoppenContext : DbContext
    {
        public webshoppenContext()
        {
        }

        public webshoppenContext(DbContextOptions<webshoppenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<OrderHistory> OrderHistories { get; set; } = null!;
        public virtual DbSet<PaymentInfo> PaymentInfos { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ShippingInfo> ShippingInfos { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:grupp3skola.database.windows.net,1433;Initial Catalog=webshoppen;Persist Security Info=False;User ID=grupp3admin;Password=NUskavikoda1234;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__City__3213A159F4FB77C6");

                entity.ToTable("City");

                entity.Property(e => e.Id).HasColumnName("ld");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Country");

                entity.HasIndex(e => e.Id, "Key");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Adress).HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.PaymentInfoId).HasColumnName("PaymentInfoID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Customer.CityId");
            });

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.ToTable("OrderHistory");
            });

            modelBuilder.Entity<PaymentInfo>(entity =>
            {
                entity.ToTable("PaymentInfo");

                entity.Property(e => e.Type).HasMaxLength(30);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.InfoText).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product.CategoryId");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Product.SupplierID");
            });

            modelBuilder.Entity<ShippingInfo>(entity =>
            {
                entity.ToTable("ShippingInfo");

                entity.Property(e => e.ParcelServiceName).HasMaxLength(30);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Supplier.CityId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
