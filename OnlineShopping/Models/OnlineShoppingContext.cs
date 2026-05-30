using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopping.Models;

public partial class OnlineShoppingContext : DbContext
{
    public OnlineShoppingContext()
    {
    }

    public OnlineShoppingContext(DbContextOptions<OnlineShoppingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddId).HasName("PK__Address__A0E1AD8E6F9191DF");

            entity.ToTable("Address");

            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LoginId).HasColumnName("LoginID");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Login).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.LoginId)
                .HasConstraintName("FK__Address__LoginID__15502E78");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B7ED9D620E");

            entity.ToTable("Cart");

            entity.HasOne(d => d.Login).WithMany(p => p.Carts)
                .HasForeignKey(d => d.LoginId)
                .HasConstraintName("FK__Cart__LoginId__24927208");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B0AB4A17F4E");

            entity.ToTable("CartItem");

            entity.Property(e => e.CartId).HasColumnName("CartID");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__CartItem__CartID__276EDEB3");

            entity.HasOne(d => d.Prd).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.PrdId)
                .HasConstraintName("FK__CartItem__PrdId__286302EC");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__Category__6A1C8AFA60D05D99");

            entity.ToTable("Category");

            entity.HasIndex(e => e.CatName, "UQ__Category__B46D3EC36D198449").IsUnique();

            entity.Property(e => e.CatName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Cat_Name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrdId).HasName("PK__Orders__67A283362D472529");

            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Login).WithMany(p => p.Orders)
                .HasForeignKey(d => d.LoginId)
                .HasConstraintName("FK__Orders__LoginId__2E1BDC42");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A16614C625");

            entity.ToTable("OrderItem");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.OrdId).HasColumnName("OrdID");
            entity.Property(e => e.PrdId).HasColumnName("PrdID");

            entity.HasOne(d => d.Ord).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__OrdID__31EC6D26");

            entity.HasOne(d => d.Prd).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.PrdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__PrdID__32E0915F");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.PrdId).HasName("PK__Product__7168B164B2A925CF");

            entity.ToTable("Product");

            entity.Property(e => e.PrdDescription)
                .IsUnicode(false)
                .HasColumnName("Prd_Description");
            entity.Property(e => e.PrdName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ScatgId).HasColumnName("SCatgId");

            entity.HasOne(d => d.Cat).WithMany(p => p.Products)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("FK__Product__CatId__1ED998B2");

            entity.HasOne(d => d.Scatg).WithMany(p => p.Products)
                .HasForeignKey(d => d.ScatgId)
                .HasConstraintName("FK__Product__SCatgId__1DE57479");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.PrdImgId).HasName("PK__ProductI__A69D8FBDBE330F34");

            entity.ToTable("ProductImage");

            entity.Property(e => e.PrdImgId).HasColumnName("PrdImg_Id");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("ImageURL");

            entity.HasOne(d => d.Prd).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.PrdId)
                .HasConstraintName("FK__ProductIm__PrdId__21B6055D");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.ScatgId).HasName("PK__SubCateg__52B89C4B124576DE");

            entity.ToTable("SubCategory");

            entity.Property(e => e.ScatgId).HasColumnName("SCatgID");
            entity.Property(e => e.SubCatgName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Cat).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("FK__SubCatego__CatId__1B0907CE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__users__4DDA2818F120EE88");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__A9D105344359B2C1").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(true)
                .HasColumnName("Is_Enabled");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
