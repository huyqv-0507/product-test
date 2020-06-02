using Microsoft.EntityFrameworkCore;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTest
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> option)
            : base(option)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(_ => _.CategoryId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
                entity.Property(_ => _.CategoryName)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.HasKey(_ => _.CategoryId);
                    
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(_ => _.ProductId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
                entity.Property(_ => _.ProductName)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(_ => _.Price)
                    .HasColumnType("decimal(17,2)")
                    .IsRequired();
                entity.Property(_ => _.Description)
                    .HasMaxLength(100);
                entity.HasKey(_ => _.ProductId);
                entity.HasOne(_ => _.Category)
                    .WithMany(_ => _.Products)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasForeignKey(_ => _.CategoryId)
                    .HasConstraintName("FK_Product_Category");
            });
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
