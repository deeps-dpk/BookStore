using Deeps.BookStore.Data;
using Deeps.BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deeps.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;


        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newbook = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                LanguageId = model.LanguageId,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl,
                UpdatedOn = DateTime.UtcNow,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow
            };
            newbook.BookGallery = new List<BookGallery>();
            foreach (var gallery in model.Gallery)
            {
                newbook.BookGallery.Add(new BookGallery()
                {
                    Name = gallery.Name,
                    Url = gallery.Url

                });
            }

            await _context.AddAsync(newbook);
            await _context.SaveChangesAsync();
            return newbook.Id;
        }
        //public async Task<List<BookModel>> GetAllBooks()
        //{
        //    var books = new List<BookModel>();
        //    var allbooks = await _context.Books.ToListAsync();
        //    if (allbooks?.Any()==true)
        //    {
        //        foreach (var book in allbooks)
        //        {
        //            books.Add(new BookModel()
        //            {
        //                Author = book.Author,
        //                Category = book.Category,
        //                Title = book.Title,
        //                Description = book.Description,
        //                Id = book.Id,
        //                LanguageId = book.LanguageId,
        //               // LanguageName = book.Language.Name,            
        //                TotalPages = book.TotalPages,                        
        //                CoverImageUrl=book.CoverImageUrl,
        //                CreatedOn = book.CreatedOn,
        //                UpdatedOn = book.UpdatedOn
        //            }); ;
        //        }               
        //    }
        //    return books;
        //}
        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Title = book.Title,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    LanguageName = book.Language.Name,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    CreatedOn = book.CreatedOn,
                    UpdatedOn = book.UpdatedOn
                }).ToListAsync();
        }
        public async Task<List<BookModel>> GetTop2Books()
        {
            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Title = book.Title,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                LanguageName = book.Language.Name,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl,
                CreatedOn = book.CreatedOn,
                UpdatedOn = book.UpdatedOn
            }).Take(2).ToListAsync();
        }
        public async Task<BookModel> GetBookbyId(int id)
        { //return DataSource().Where(x => x.Id == id).FirstOrDefault();
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    LanguageName = book.Language.Name,
                    Title = book.Title,
                    BookPdfUrl = book.BookPdfUrl,
                    Gallery = book.BookGallery.Select(x => new GalleryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Url = x.Url
                    }).ToList(),
                    TotalPages = book.TotalPages,
                    CreatedOn = book.CreatedOn,
                    UpdatedOn = book.UpdatedOn
                }).FirstOrDefaultAsync();
        }

        //public List<BookModel> SearchBooks(string title, string authorname)

        //{           
        //    return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorname)).ToList();
        //}
        public async Task<int> DeleteBookbyId(int id)
        { //return DataSource().Where(x => x.Id == id).FirstOrDefault();
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return id;
        }

        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){ Id=1, Author="Pradeep", Title="MVC", Description="This is about MVC Description", Category="Action", Language="English", TotalPages=333 },
        //        new BookModel(){ Id=2, Author="Prasanth", Title="C#", Description="This is about C# Description",Category="Drama", Language="Hindi", TotalPages=444},
        //        new BookModel(){ Id=3, Author="Latha", Title="WebAPI", Description="This is about WebAPI Description",Category="Romance", Language="Telugu", TotalPages=555},
        //        new BookModel(){ Id=4, Author="Emily", Title="Core", Description="This is about Core Description",Category="Comedy", Language="Urdu", TotalPages=666},
        //        new BookModel(){ Id=5, Author="Joe", Title="Python", Description="This is about Python Description",Category="Adventure", Language="Tamil", TotalPages=777},
        //        new BookModel(){ Id=6, Author="Praveen", Title="PHP", Description="This is about PHP Description",Category="Sci-Fi", Language="Japanese", TotalPages=888},
        //        new BookModel(){ Id=7, Author="Anil", Title="Dotnet", Description="This is about Dotnet Description",Category="Thriller", Language="Chinese", TotalPages=999},
        //    };
        //}

    }
}
