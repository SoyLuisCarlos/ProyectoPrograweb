using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoPrograweb.Models.dbModels
{
    public partial class ProyectoContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ProyectoContext()
        {
        }

        public ProyectoContext(DbContextOptions<ProyectoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<NewsCategory> NewsCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Proyecto;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.IdFeedback).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.IdMatch).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdNewsCategoryNavigation)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.IdNewsCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_NewsCategory");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.IdNews).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdNewsCategoryNavigation)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.IdNewsCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_News_NewsCategory");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_News_Users");
            });

            modelBuilder.Entity<NewsCategory>(entity =>
            {
                entity.Property(e => e.IdNewsCategory).ValueGeneratedNever();
            });
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
