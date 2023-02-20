using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private BookstoreUnitOfWork data { get; set; }
        public BookController(BookstoreContext ctx) => data = new BookstoreUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(BooksGridDTO values)
        {
            var builder = new BooksGridBuilder(HttpContext.Session, values, 
                defaultSortField: nameof(Book.Title));

            var options = new BookQueryOptions {
                Include = "BookAuthors.Author, Genre",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new BookListViewModel {
                Books = data.Books.List(options),
                Authors = data.Authors.List(new QueryOptions<Author> {
                    OrderBy = a => a.FirstName }),
                Genres = data.Genres.List(new QueryOptions<Genre> {
                    OrderBy = g => g.Name }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Books.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var book = data.Books.Get(new QueryOptions<Book> {
                Include = "BookAuthors.Author, Genre",
                Where = b => b.BookId == id
            });
            return View(book);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new BooksGridBuilder(HttpContext.Session);

            if (clear) {
                builder.ClearFilterSegments();
            }
            else {
                var author = data.Authors.Get(filter[0].ToInt());
                builder.LoadFilterSegments(filter, author);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }   
}