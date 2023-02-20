using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private Repository<Author> data { get; set; }
        public AuthorController(BookstoreContext ctx) => data = new Repository<Author>(ctx);

        public IActionResult Index() => RedirectToAction("List");

        // dto has properties for the paging and sorting route segments defined in the Startup.cs file
        public ViewResult List(GridDTO vals)
        {
            // get grid builder, which loads route segment values and stores them in session
            string defaultSort = nameof(Author.FirstName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            // create options for querying authors. OrderBy depends on value in SortField route 
            var options = new QueryOptions<Author> {
                Includes = "BookAuthors.Book",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.FirstName;
            else
                options.OrderBy = a => a.LastName;

            // create view model and add page of author data, the current route,
            // and the total number of pages
            var vm = new AuthorListViewModel {
                Authors = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            // pass view model to view
            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var author = data.Get(new QueryOptions<Author> {
                Includes = "BookAuthors.Book",
                Where = a => a.AuthorId == id
            });
            return View(author);
        }
    }
}