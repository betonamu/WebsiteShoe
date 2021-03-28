using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteShoe.Entities
{
    public class ShoeDbContext : DbContext
    {
        public ShoeDbContext(DbContextOptions<ShoeDbContext> ops) : base(ops)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BillDetail>(entity =>
            {
                entity.HasKey(x => new { x.BillId, x.ShoeId });

                entity.HasOne<Bill>(b => b.Bill)
                .WithMany(s => s.BillDetails)
                .HasForeignKey(sc => sc.BillId);

                entity.HasOne<Shoe>(b => b.Shoe)
                .WithMany(s => s.BillDetails)
                .HasForeignKey(sc => sc.ShoeId);
            });

            modelBuilder.Entity<ShoeDetail>(entity =>
            {
                entity.HasKey(x => new { x.ShoeId, x.SizeId });

                entity.HasOne<Shoe>(s => s.Shoe)
                .WithMany(s => s.ShoeDetails)
                .HasForeignKey(sc => sc.ShoeId);

                entity.HasOne<ShoeSize>(s => s.ShoeSize)
                .WithMany(s => s.ShoeDetails)
                .HasForeignKey(sc => sc.SizeId);
            });

            modelBuilder.Entity<Shoe>(entity =>
            {
                entity.HasKey(s => s.ShoeId)
                .HasName("PK_Shoe");

                entity.HasOne<ShoeStyle>(s => s.ShoeStyle)
                .WithMany(sc => sc.Shoes)
                .HasForeignKey(sc => sc.StyleId);

            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.HasKey(s => s.ProducerId)
                .HasName("PK_Producer");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(b => b.BillId)
                .HasName("PK_Bill");

                entity.HasOne(c => c.Customer)
                .WithMany(b => b.Bills)
                .HasForeignKey(sc => sc.CustomerId);
            });

            modelBuilder.Entity<ShoeStyle>(entity =>
            {
                entity.HasKey(c => c.StyleId)
                .HasName("PK_ShoeStyle");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId)
                .HasName("PK_Customer");

                entity.HasIndex(c => c.UserName)
                .IsUnique();
            });

            modelBuilder.Entity<ShoeSize>(entity =>
            {
                entity.HasKey(x => x.SizeId)
                .HasName("PK_ShoeSize");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(x => x.Id)
                .HasName("PK_Id");
            });
        }

        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<ShoeStyle> ShoeStyles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoeSize> ShoeSizes { get; set; }
        public DbSet<ShoeDetail> ShoeDetails { get; set; }
        public DbSet<Admin> Admins { get; set; }

    }

}
