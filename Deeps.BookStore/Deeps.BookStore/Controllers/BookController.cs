using Deeps.BookStore.Data;
using Deeps.BookStore.Models;
using Deeps.BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Deeps.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
           _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ViewResult> GetAllBooks(bool issuccess = false, int bookid = 0)
        {
            ViewBag.issuccess = issuccess;
            ViewBag.bookid = bookid;
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }
        [Route("book-details/{id}", Name = "bookdetailsroute")]
        public async Task<ViewResult> GetBookbyId(int id)
        {
            var data = await _bookRepository.GetBookbyId(id);
            return View(data);
        }
        [Route("delete-book/{id}", Name = "deletebookroute")]
        public async Task<IActionResult> DeleteBookbyId(int id)
        {
            int book = await _bookRepository.DeleteBookbyId(id);
            if (book > 0)
            {
                return RedirectToAction(nameof(GetAllBooks), new { issuccess = true, bookid = id });
            }
            return View();
        }
        //public ViewResult SearchBooks(string title, string author)
        //{
        //    var data = _bookRepository.SearchBooks(title, author);
        //    return View(data);
        //}
        public async Task<ViewResult> AddNewBook(bool issuccess = false, int bookid = 0)
        {
            //var model = new BookModel()
            //{
            //    Language = "3"
            //};
            //ViewBag.Language = new List<string>(){ "Hindi", "English", "Chinese", "Japanese", "Tamil" };
            //ViewBag.Language = new SelectList(Language(), "Japanese");
            //ViewBag.GetLanguages = new SelectList(GetLanguages(), "Id", "Text");
            //ViewBag.GetLanguages = GetLanguages().Select(X => new SelectListItem()
            //{
            //    Value = X.Id.ToString(),
            //    Text=X.Text
            //}).ToList();

            //var group1 = new SelectListGroup() { Name = "group1" };
            //var grou2 = new SelectListGroup() { Name = "group2", Disabled=true };

            //ViewBag.GetLanguages2 = new List<SelectListItem>() 
            //{ 
            //    new SelectListItem() {Text="Chinese", Value="1" , Group=group1},
            //    new SelectListItem() {Text="Japanese", Value="2", Selected=true, Group=group1 },
            //    new SelectListItem() {Text="English", Value="3", Group=grou2 },
            //    new SelectListItem() {Text="Korean", Value="4", Group=grou2 }
            //};

            //ViewBag.GetLanguages2 = new List<SelectListItem>()
            //{
            //    new SelectListItem() {Text="Chinese", Value="1" },
            //    new SelectListItem() {Text="Japanese", Value="2", Selected=true},
            //    new SelectListItem() {Text="English", Value="3"},
            //    new SelectListItem() {Text="Tamil", Value="4"},
            //    new SelectListItem() {Text="urdu", Value="5"},
            //    new SelectListItem() {Text="Korean", Value="6"}
            //};
            //ViewBag.Language = Language();
           // ViewBag.GetLanguages = new SelectList(await _languageRepository.GetLanguages(),"Id", "Name");
            ViewBag.issuccess = issuccess;
            ViewBag.bookid = bookid;

            return View();
         }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {               
                if (bookModel.CoverPhoto != null)
                {
                    string folderpath = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folderpath, bookModel.CoverPhoto);
                }
                if (bookModel.BookPdf != null)
                {
                    string folderpath = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folderpath, bookModel.BookPdf);
                }
                if (bookModel.GalleryFiles != null)
                {
                    string folderpath = "books/gallery/";
                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            Url = await UploadImage(folderpath, file)
                        };
                        bookModel.Gallery.Add(gallery);

                    }

                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { issuccess = true, bookid = id });
                }
            }
            //ViewBag.Language = Language();
            //ViewBag.Language = new SelectList(Language(), "Japanese");
            // ViewBag.GetLanguages = new SelectList(GetLanguages(), "Id", "Text");

            //ViewBag.GetLanguages2 = new List<SelectListItem>()
            //{
            //    new SelectListItem() {Text="Chinese", Value="1" },
            //    new SelectListItem() {Text="Japanese", Value="2", Selected=true},
            //    new SelectListItem() {Text="English", Value="3"},
            //    new SelectListItem() {Text="Tamil", Value="4"},
            //    new SelectListItem() {Text="urdu", Value="5"},
            //    new SelectListItem() {Text="Korean", Value="6"}
            //};
           // ViewBag.GetLanguages = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            return View();
        }

        private async Task<string> UploadImage(string folderpath, IFormFile file)
        {

            folderpath += Guid.NewGuid().ToString() +"_"+ file.FileName;
            string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folderpath);
            await file.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            return "/" + folderpath;
        }

        //private List<LanguageModel> GetLanguages()
        // {
        //     return new List<LanguageModel>()
        //     {
        //         new LanguageModel(){Id=1, Text="English"},
        //         new LanguageModel(){Id=2, Text="Hindi"},
        //         new LanguageModel(){Id=3, Text="Urdu"},
        //         new LanguageModel(){Id=4, Text="Telugu"},
        //         new LanguageModel(){Id=5, Text="Tamil"}
        //     };
        // }
        //private List<string> Language()
        //{
        //    return new List<string>()
        //    {
        //        "Hindi", "English", "Chinese", "Japanese", "Tamil"
        //    };
        //}
    }
}
