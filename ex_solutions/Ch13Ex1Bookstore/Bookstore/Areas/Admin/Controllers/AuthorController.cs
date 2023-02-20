using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private Repository<Author> data { get; set; }
        public AuthorController(BookstoreContext ctx) => data = new Repository<Author>(ctx);

        public ViewResult Index()
        {
            var authors = data.List(new QueryOptions<Author> {
                OrderBy = a => a.FirstName
            });
            return View(authors);
        }

        // select (posted from author drop down on Index page). 
        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "view books":
                    return RedirectToAction("ViewBooks", new { id });
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        // add
        [HttpGet]
        public ViewResult Add() => View("Author", new Author());

        [HttpPost]
        public IActionResult Add(Author author, string operation)
        {
            // server-side version of remote validation 
            var validate = new Validate(TempData);
            if (!validate.IsAuthorChecked) {
                validate.CheckAuthor(author.FirstName, author.LastName, operation, data);
                if (!validate.IsValid) {
                    ModelState.AddModelError(nameof(author.LastName), validate.ErrorMessage);
                }    
            }
            
            if (ModelState.IsValid) {
                data.Insert(author);
                data.Save();
                validate.ClearAuthor();
                TempData["message"] = $"{author.FullName} added to Authors.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else {
                return View("Author", author);
            }
        }

        // edit
        [HttpGet]
        public ViewResult Edit(int id) => View("Author", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            // no remote validation of author on edit
            if (ModelState.IsValid) {
                data.Update(author);
                data.Save();
                TempData["message"] = $"{author.FullName} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else {
                return View("Author", author);
            }
        }

        // delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var author = data.Get(new QueryOptions<Author> {
                Includes = "BookAuthors",
                Where = a => a.AuthorId == id
            });

            if (author.BookAuthors.Count > 0) {
                TempData["message"] = $"Can't delete author {author.FullName} because they are associated with these books.";
                return GoToAuthorSearch(author);
            }
            else {
                return View("Author", author);
            }
        }

        [HttpPost]
        public RedirectToActionResult Delete(Author author)
        {
            // no ModelState.IsValid check here bc there's no user input - 
            // only AuthorId in hidden field is posted from form.  
            data.Delete(author);
            data.Save();
            TempData["message"] = $"{author.FullName} removed from Authors.";
            return RedirectToAction("Index");  // PRG pattern
        }

        // view books by author
        public RedirectToActionResult ViewBooks(int id)
        {
            var author = data.Get(id);
            return GoToAuthorSearch(author);
        }

        // private helper method
        private RedirectToActionResult GoToAuthorSearch(Author author)
        {
            // store author search data in TempData and redirect
            var search = new SearchData(TempData) {
                SearchTerm = author.FullName,
                Type = "author"
            };
            return RedirectToAction("Search", "Book");
        }
    }
}