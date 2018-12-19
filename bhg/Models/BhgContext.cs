﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public partial class BhgContext : DbContext
    {
        public virtual DbSet<TreasureMapEntity> TreasureMap { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }

        public BhgContext(DbContextOptions<BhgContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreasureMap>(entity =>
            {
                //entity.Property(e => e.TreasureMapId).HasColumnName("TreasureMapId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("double");
                entity.Property(e => e.LatitudeDelta).HasColumnType("double");
                entity.Property(e => e.Longitude).HasColumnType("double");
                entity.Property(e => e.LongitudeDelta).HasColumnType("double");
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

                entity.Property(e => e.Latitude).HasColumnType("double");
                entity.Property(e => e.Longitude).HasColumnType("double");

                entity.Property(e => e.Notes)
                    .IsUnicode(true);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.ModDate).HasColumnType("datetime");
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