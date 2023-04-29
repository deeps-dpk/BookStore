using Deeps.BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Deeps.BookStore.Controllers
{
    public interface IHomeController
    {
        BookModel Book { get; set; }
        string Property { get; set; }
        string Title { get; set; }

        ViewResult AboutUs();
        ViewResult ContactUs(int id);
        ViewResult Index();
    }
}