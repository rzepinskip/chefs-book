﻿using System;
using ChefsBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsBook.Core
{
    public class CoreDbContext : DbContext
    {
        public const string Schema = "core";

        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Step> Steps => Set<Step>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<RecipeTag> RecipeTags => Set<RecipeTag>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<Recipe>(cfg =>
            {
                cfg.HasKey(e => e.RecipeId);

                cfg.Property(e => e.Title).HasMaxLength(200);
                cfg.Property(e => e.Description).HasMaxLength(4000);
                cfg.Property(e => e.Image).HasMaxLength(2000);
                cfg.Property(e => e.Notes).HasMaxLength(2000);

                cfg.HasMany(e => e.Ingredients);
                cfg.HasMany(e => e.Steps);
                cfg.HasMany(e => e.Tags);
            });

            modelBuilder.Entity<Ingredient>(cfg =>
            {
                cfg.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Step>(cfg =>
            {
                cfg.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Tag>(cfg =>
            {
                cfg.HasKey(t => t.Id);
                cfg.Property(e => e.Name).HasMaxLength(100);

                cfg
                    .HasMany(t => t.Recipes)
                    .WithOne(ct => ct.Tag)
                    .HasForeignKey(ct => ct.TagId);
            });

            modelBuilder.Entity<RecipeTag>(cfg =>
            {
                cfg.HasKey(t => new { t.TagId, t.RecipeId });
            });

            modelBuilder.Entity<CartItem>(cfg => 
            {
                cfg.HasKey(i => new { i.UserId, i.RecipeId });
            });
        }
    }
}
