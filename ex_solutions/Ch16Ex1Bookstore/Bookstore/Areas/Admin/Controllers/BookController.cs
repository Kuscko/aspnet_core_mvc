using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Models;

namespace Bookstore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BookController : Controller
    {
        private BookstoreUnitOfWork data { get; set; }
        public BookController(BookstoreContext ctx) => data = new BookstoreUnitOfWork(ctx);

        public ViewResult Index() {
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid) {
                var search = new SearchData(TempData) {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }  
            else {
                return RedirectToAction("Index");
            }   
        }

        [HttpGet]
        public ViewResult Search() 
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm) {
                var vm = new SearchViewModel {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Book> {
                    Include = "Genre, BookAuthors.Author"
                };
                if (search.IsBook) { 
                    options.Where = b => b.Title.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for book title '{vm.SearchTerm}'";
                }
                if (search.IsAuthor) {
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1) {
                        options.Where = b => b.BookAuthors.Any(
                            ba => ba.Author.FirstName.Contains(vm.SearchTerm) || 
                            ba.Author.LastName.Contains(vm.SearchTerm));
                    }
                    else {
                        string first = vm.SearchTerm.Substring(0, index);
                        string last = vm.SearchTerm.Substring(index + 1); 
                        options.Where = b => b.BookAuthors.Any(
                            ba => ba.Author.FirstName.Contains(first) && 
                            ba.Author.LastName.Contains(last));
                    }
                    vm.Header = $"Search results for author '{vm.SearchTerm}'";
                }
                if (search.IsGenre) {                  
                    options.Where = b => b.GenreId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for genre ID '{vm.SearchTerm}'";
                }
                vm.Books = data.Books.List(options);
                return View("SearchResults", vm);
            }
            else {
                return View("Index");
            }     
        }

        [HttpGet]
        public ViewResult Add(int id) => GetBook(id, "Add");

        [HttpPost]
        public IActionResult Add(BookViewModel vm)
        {
            if (ModelState.IsValid) {
                data.LoadNewBookAuthors(vm.Book, vm.SelectedAuthors);
                data.Books.Insert(vm.Book);
                data.Save();

                TempData["message"] = $"{vm.Book.Title} added to Books.";
                return RedirectToAction("Index");  
            }
            else {
                Load(vm, "Add");
                return View("Book", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetBook(id, "Edit");
        
        [HttpPost]
        public IActionResult Edit(BookViewModel vm)
        {
            if (ModelState.IsValid) {
                data.DeleteCurrentBookAuthors(vm.Book);
                data.LoadNewBookAuthors(vm.Book, vm.SelectedAuthors);
                data.Books.Update(vm.Book);
                data.Save(); 
                
                TempData["message"] = $"{vm.Book.Title} updated.";
                return RedirectToAction("Search");  
            }
            else {
                Load(vm, "Edit");
                return View("Book", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetBook(id, "Delete");

        [HttpPost]
        public IActionResult Delete(BookViewModel vm)
        {
            data.Books.Delete(vm.Book); 
            data.Save();
            TempData["message"] = $"{vm.Book.Title} removed from Books.";
            return RedirectToAction("Search");  
        }

        private ViewResult GetBook(int id, string operation)
        {
            var book = new BookViewModel();
            Load(book, operation, id);
            return View("Book", book);
        }
        private void Load(BookViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op)) { 
                vm.Book = new Book();
            }
            else {
                vm.Book = data.Books.Get(new QueryOptions<Book>
                {
                    Include = "BookAuthors.Author, Genre",
                    Where = b => b.BookId == (id ?? vm.Book.BookId)
                });
            }

            vm.SelectedAuthors = vm.Book.BookAuthors?.Select(
                ba => ba.Author.AuthorId).ToArray();
            vm.Authors = data.Authors.List(new QueryOptions<Author> {
                OrderBy = a => a.FirstName });
            vm.Genres = data.Genres.List(new QueryOptions<Genre> {
                    OrderBy = g => g.Name });
        }

    }
}