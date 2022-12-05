using System;
using System.Collections.Generic;
using Film_laba1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Film_laba1
{
    public partial class DBFilmsContext : DbContext
    {
        public DBFilmsContext()
        {
        }

        public DBFilmsContext(DbContextOptions<DBFilmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Filmcompany> Filmcompanies { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Producer> Producers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-P972F3E;\nDatabase=DBFilms; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("Film");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FilmCompanyId).HasColumnName("FilmCompanyID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Info).HasColumnName("info");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ProducerId).HasColumnName("ProducerID");

                entity.HasOne(d => d.FilmCompany)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.FilmCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Film_Filmcompany");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_Film_Genre");

                entity.HasOne(d => d.Producer)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.ProducerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Film_Producer");
            });

            modelBuilder.Entity<Filmcompany>(entity =>
            {
                entity.ToTable("Filmcompany");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.ToTable("Producer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Info).HasColumnName("info");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Producers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producer_Country");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
