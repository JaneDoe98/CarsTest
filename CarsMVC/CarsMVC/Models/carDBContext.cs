using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarsMVC.Models
{
    public partial class carDBContext : DbContext
    {
        public carDBContext()
        {
        }

        public carDBContext(DbContextOptions<carDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Ownership> Ownerships { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("cars");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("brand");

                entity.Property(e => e.ProductionDate)
                    .HasColumnType("date")
                    .HasColumnName("production_date");

                entity.Property(e => e.RegistrationNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("registration_number");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("owners");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Surname)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Ownership>(entity =>
            {
                entity.ToTable("ownerships");

                entity.Property(e => e.OwnershipId).HasColumnName("ownership_id");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Ownerships)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK__ownership__car_i__5070F446");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Ownerships)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__ownership__owner__4F7CD00D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
