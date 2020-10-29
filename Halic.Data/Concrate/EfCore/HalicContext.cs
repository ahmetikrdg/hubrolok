using Halic.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Data.Concrate.EfCore
{
    public class HalicContext:DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet <Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<News> News { get; set; }               
        public DbSet<NCategory> NCategories { get; set; }
        public DbSet<Activities> Activities { get; set; }
        public DbSet<EMail> EMails { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=DBHalicHubUp;integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleCategory>()
            .HasKey(c => new { c.CategoryId, c.ArticleId });

            modelBuilder.Entity<ArticleAuthor>()
           .HasKey(c => new { c.ArticleId, c.AuthorId });

            modelBuilder.Entity<NewsHCategory>()
            .HasKey(c => new { c.NewsId, c.NCategoryId });  

            modelBuilder.Entity<NewsAuthor>()
           .HasKey(c => new { c.NewsId, c.AuthorId });
        }
    }
}
