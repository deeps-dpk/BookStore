using Deeps.BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Deeps.BookStore.Controllers
{
    public interface IBookController
    {
        Task<ViewResult> AddNewBook(bool issuccess = false, int bookid = 0);
        Task<IActionResult> AddNewBook(BookModel bookModel);
        Task<IActionResult> DeleteBookbyId(int id);
        Task<ViewResult> GetAllBooks(bool issuccess = false, int bookid = 0);
        Task<ViewResult> GetBookbyId(int id);
    }
}