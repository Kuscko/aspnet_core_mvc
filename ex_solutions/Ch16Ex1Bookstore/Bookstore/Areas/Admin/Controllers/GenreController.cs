using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Models;

namespace Bookstore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GenreController : Controller
    {
        private Repository<Genre> data { get; set; }
        public GenreController(BookstoreContext ctx) => data = new Repository<Genre>(ctx);

        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            var genres = data.List(new QueryOptions<Genre> {
                OrderBy = g => g.Name
            });
            return View(genres);
        }

        [HttpGet]
        public ViewResult Add() => View("Genre", new Genre());

        [HttpPost]
        public IActionResult Add(Genre genre)
        {
            var validate = new Validate(TempData);
            if (!validate.IsGenreChecked) {
                validate.CheckGenre(genre.GenreId, data);
                if (!validate.IsValid) {
                    ModelState.AddModelError(nameof(genre.GenreId), validate.ErrorMessage);
                }     
            }

            if (ModelState.IsValid) {
                data.Insert(genre);
                data.Save();
                validate.ClearGenre();
                TempData["message"] = $"{genre.Name} added to Genres.";
                return RedirectToAction("Index");  
            }
            else {
                return View("Genre", genre);
            }
        }

        [HttpGet]
        public ViewResult Edit(string id) => View("Genre", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid) {
                data.Update(genre);
                data.Save();
                TempData["message"] = $"{genre.Name} updated.";
                return RedirectToAction("Index");  
            }
            else {
                return View("Genre", genre);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id) {
            var genre = data.Get(new QueryOptions<Genre> {
                Include = "Books",
                Where = g => g.GenreId == id
            });

            if (genre.Books.Count > 0) {
                TempData["message"] = $"Can't delete genre {genre.Name} " 
                                    + "because it's associated with these books.";
                return GoToBookSearchResults(id);
            }
            else {
                return View("Genre", genre);
            }
        }

        [HttpPost]
        public IActionResult Delete(Genre genre)
        {
            data.Delete(genre);
            data.Save();
            TempData["message"] = $"{genre.Name} removed from Genres.";
            return RedirectToAction("Index");  
        }

        public RedirectToActionResult ViewBooks(string id) => GoToBookSearchResults(id);

        private RedirectToActionResult GoToBookSearchResults(string id)
        {
            var search = new SearchData(TempData) {
                SearchTerm = id,
                Type = "genre"
            };
            return RedirectToAction("Search", "Book");
        }

    }
}