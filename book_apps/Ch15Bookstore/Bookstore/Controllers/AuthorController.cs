using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private IRepository<Author> data { get; set; }
        public AuthorController(IRepository<Author> rep) => data = rep;

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {
            string defaultSort = nameof(Author.FirstName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

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

            var vm = new GridViewModel<Author> {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

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