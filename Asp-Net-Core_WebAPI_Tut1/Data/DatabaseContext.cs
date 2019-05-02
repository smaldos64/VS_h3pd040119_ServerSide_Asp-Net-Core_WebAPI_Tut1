using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Net_Core_WebAPI_Tut1.Models;

namespace Asp_Net_Core_WebAPI_Tut1.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet <AuthorContact> AuthorContacts { get; set; }
        public DbSet <Book> Books { get; set; }
        public DbSet <BookAuthor> BookAuthors { get; set; }
        public DbSet <BookCategory> BookCategories { get; set; }
        public DbSet <Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(c => new { c.AuthorID, c.BookID });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebApiCoreDatabase_1;Trusted_Connection=True;");

            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder
            //        .UseLazyLoadingProxies()
            //        .UseSqlServer("Server=.;Database=WebApiCoreDatabase_1;Trusted_Connection=True;");
            //}
        }
    }
}
