using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public partial class BhgContext : DbContext
    {
        public virtual DbSet<TreasureMap> TreasureMap { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }

        public BhgContext(DbContextOptions<BhgContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreasureMap>(entity =>
            {
                entity.Property(e => e.TreasureMapId).HasColumnName("TreasureMapId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal");
                entity.Property(e => e.LatitudeDelta).HasColumnType("decimal");
                entity.Property(e => e.Longitude).HasColumnType("decimal");
                entity.Property(e => e.LongitudeDelta).HasColumnType("decimal");
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ModDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(e => e.PlaceId).HasColumnName("PlaceId");

                entity.Property(e => e.TreasureMapId).HasColumnName("TreasureMapId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Latitude).HasColumnType("decimal");
                entity.Property(e => e.Longitude).HasColumnType("decimal");

                entity.Property(e => e.Notes)
                    .IsUnicode(true);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ModDate).HasColumnType("datetime");

                entity.HasOne(d => d.TreasureMap)
                    .WithMany(p => p.Place)
                    .HasForeignKey(d => d.TreasureMapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Place_TreasureMap");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentId");

                entity.Property(e => e.PlaceId).HasColumnName("PlaceId");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(true);

                entity.Property(e => e.Notes)
                    .IsUnicode(true);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Attachment)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attachment_Place");
            });
        }
    }
}