using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deeps.BookStore.Data
{
    public class BookStoreContext:DbContext
    {
       
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=DJ\SQLEXPRESS;Database:BookStore;Integrated Security=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
