using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bhg.Models
{
    public partial class BhgContext : DbContext
    {
        public virtual DbSet<TreasureMapEntity> TreasureMaps { get; set; }
        public virtual DbSet<GemEntity> Gems { get; set; }
        public virtual DbSet<RouteLineEntity> RouteLines { get; set; }
        public virtual DbSet<AttachmentEntity> Attachments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<IconEntity> Icons { get; set; }
        public BhgContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<TreasureMapEntity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");

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

            modelBuilder.Entity<GemEntity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.IconId).HasColumnName("IconId");

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

                entity.HasOne(d => d.TreasureMap)
                .WithMany(p => p.Gems)
                .HasForeignKey(d => d.TreasureMapId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gems_TreasureMapId");

                entity.HasOne(a => a.Icon)
                .WithMany(b => b.Gems)
                .HasForeignKey(c => c.IconId);
            });

            modelBuilder.Entity<RouteLineEntity>(entity =>
            {
                entity.Property(e => e.TreasureMapId).HasColumnName("TreasureMapId");
                entity.Property(e => e.StartLatitude).HasColumnType("double");
                entity.Property(e => e.StartLongitude).HasColumnType("double");
                entity.Property(e => e.EndLatitude).HasColumnType("double");
                entity.Property(e => e.EndLongitude).HasColumnType("double");

                entity.HasOne(d => d.TreasureMap)
                .WithMany(p => p.RouteLines)
                .HasForeignKey(d => d.TreasureMapId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RouteLines_TreasureMapId");
            });

            modelBuilder.Entity<AttachmentEntity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.GemId).HasColumnName("GemId");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(true);

                entity.Property(e => e.Notes)
                    .IsUnicode(true);

                entity.HasOne(d => d.Gem)
                .WithMany(p => p.Attachments)
                .HasForeignKey(d => d.GemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attachments_GemId");
            });

            modelBuilder.Entity<IconEntity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(true);                
            });

            //modelBuilder.Entity<IconEntity>()
            //    .HasMany(a => a.Gems)
            //    .WithOne(b => b.Icon)
            //    .HasForeignKey(c => c.IconId);

            //modelBuilder.Entity<GemEntity>()
            //    .HasOne(a => a.Icon)
            //    .WithMany(b => b.Gems)
            //    .HasForeignKey(c => c.IconId);

        }
    }
}