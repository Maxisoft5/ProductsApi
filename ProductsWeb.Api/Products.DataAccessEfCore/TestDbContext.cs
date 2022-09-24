using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Products.DTO;

namespace Products.DataAccessEfCore
{
    public partial class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventLog> EventLogs { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductVersion> ProductVersions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=TestDb;Trusted_Connection=True;Integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FindProductDTOResult>(e =>
            {
                e.HasNoKey();
            });

            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.ToTable("EventLog");

                entity.HasIndex(e => e.EventDate, "EventLogDescription");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EventDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name, "ProductName");

                entity.HasIndex(e => e.Name, "UQ__Products__737584F6B98CCC61")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(225);
            });

            modelBuilder.Entity<ProductVersion>(entity =>
            {
                entity.HasIndex(e => e.CreatingDate, "ProductVersionCreatingDate");

                entity.HasIndex(e => e.Height, "ProductVersionHeight");

                entity.HasIndex(e => e.Length, "ProductVersionLength");

                entity.HasIndex(e => e.Width, "ProductVersionWidth");

                entity.HasIndex(e => e.Name, "ProductVersionsName");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatingDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(225);

                entity.Property(e => e.Height).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Length).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name).HasMaxLength(225);

                entity.Property(e => e.Width).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVersions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ProductVe__Produ__276EDEB3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
