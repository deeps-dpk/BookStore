using Deeps.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deeps.BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<int> DeleteBookbyId(int id);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookbyId(int id);
        Task<List<BookModel>> GetTop2Books();
    }
}