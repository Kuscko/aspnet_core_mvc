using System;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Book> data { get; set; }
        public HomeController(IRepository<Book> rep) => data = rep;

        public ViewResult Index()
        {
            var random = data.Get(new QueryOptions<Book> {
                OrderBy = b => Guid.NewGuid()
            });

            return View(random);
        }

        public ContentResult Register()
        {
            return Content("Registration has not been implemented yet. It is implemented in chapter 16.");
        }

    }
}