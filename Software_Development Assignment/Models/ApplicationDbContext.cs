using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Software_Development_Assignment.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FavoriteFood> FavoriteFoods { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Respondent> Respondents { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteFood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC07C5A5D005");

            entity.Property(e => e.FoodName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Respondent).WithMany(p => p.FavoriteFoods)
                .HasForeignKey(d => d.RespondentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteF__Respo__3B75D760");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ratings__3214EC0781769A91");

            entity.Property(e => e.Question)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rating1).HasColumnName("Rating");

            entity.HasOne(d => d.Respondent).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RespondentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ratings__Respond__3F466844");
        });

        modelBuilder.Entity<Respondent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Responde__3214EC0751F55A89");

            entity.Property(e => e.ContactNumber)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
