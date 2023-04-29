using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Deeps.BookStore.Models;

namespace Deeps.BookStore.Controllers
{
    public class HomeController:Controller
    {
        [ViewData]
        public string Property { get; set; }
       [ViewData]
        public string Title { get; set; }
        [ViewData]
        public BookModel Book { get; set; }
        public ViewResult Index()
        {
            ViewBag.name = "deeps vewbag";
            Property = "Custom Property";
            Title = "Title from Index AM";

            dynamic data = new ExpandoObject();
            data.id = 3;
            data.name = "deeps";
            ViewBag.data = data;

            ViewBag.Type = new BookModel() { Id = 11, Title = "Pradeep Kumar" };
            Book = new BookModel() { Id = 33333, Author = "Adventure" };
            return View();
                
        }
        public ViewResult AboutUs()
        {
            ViewData["name"] = "Pradeep";
            ViewData["id"] = 333;
            ViewData["book"] = new BookModel() { Id = 3, Title = "Prasanth" };
            
            ViewBag.Title = "Title(viebag->viewdata)";
            Title = "Title from AboutUs AM";
            return View();

        }
        [Route("contact-us/{name?}", Name ="cus")]
        public ViewResult ContactUs(int id)
        {
           Title = "Title from ContactUs AM";
            if (id>0)
            {
                ViewBag.id = id;
            }
            else
            {
                ViewBag.id = null;
            }
            
            return View();          

        }
    }
}
