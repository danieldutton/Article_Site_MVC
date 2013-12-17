﻿using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArticleSite.DataAccess.Interfaces;
using ArticleSite.Model.Entities;
using System.Data.Entity;

namespace ArticleSite.DataAccess
{
    public class ArticleDbContext : DbContext, IDbContext
    {
        public virtual IDbSet<Article> Articles { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Subscriber> Subscribers { get; set; }

        public virtual IDbSet<NewsLetter> NewsLetters { get; set; }


        public ArticleDbContext() : base("ArticleDb")
        {           
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public DbEntityEntry Entry(object o)
        {
            return base.Entry(o);
        } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Article>()
                         .HasMany(j => j.Categories)
                         .WithMany(j => j.Articles)
                         .Map(x => x.ToTable("ArticleCategory"));

            ArticleDbContext db = new ArticleDbContext();
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
