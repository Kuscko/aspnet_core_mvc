using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private IRepository<Book> data { get; set; }
        public BookController(IRepository<Book> rep) => data = rep;

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(BooksGridDTO values)
        {
            var builder = new BooksGridBuilder(HttpContext.Session, values, 
                defaultSortField: nameof(Book.Title));

            var options = new BookQueryOptions {
                Includes = "BookAuthors.Author, Genre",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new GridViewModel<Book> {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var book = data.Get(new QueryOptions<Book> {
                Includes = "BookAuthors.Author, Genre",
                Where = b => b.BookId == id
            });
            return View(book);
        }

        [HttpPost]
        public RedirectToActionResult Filter([FromServices]IRepository<Author> data, 
            string[] filter, bool clear = false)
        {
            var builder = new BooksGridBuilder(HttpContext.Session);

            if (clear) {
                builder.ClearFilterSegments();
            }
            else {
                var author = data.Get(filter[0].ToInt());
                builder.LoadFilterSegments(filter, author);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }   
}