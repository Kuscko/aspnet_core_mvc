using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private BookstoreUnitOfWork data { get; set; }
        public BookController(BookstoreContext ctx) => data = new BookstoreUnitOfWork(ctx);

        public ViewResult Index() {
            // clear any previous searches
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        // search (posted from search text box on Index page). Search term is required field.
        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid) {
                // store search data in TempData and redirect
                var search = new SearchData(TempData) {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search"); // PRG pattern
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
                    Includes = "Genre, BookAuthors.Author"
                };
                if (search.IsBook) { 
                    options.Where = b => b.Title.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for book title '{vm.SearchTerm}'";
                }
                if (search.IsAuthor) {
                    // if there's no space, search both first and last name by search term. 
                    // Otherwise, assume there's a first and last name and refine search.
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1) { // no space
                        options.Where = b => b.BookAuthors.Any(
                            ba => ba.Author.FirstName.Contains(vm.SearchTerm) ||  // OR
                            ba.Author.LastName.Contains(vm.SearchTerm));
                    }
                    else { // assume first and last name
                        string first = vm.SearchTerm.Substring(0, index);
                        string last = vm.SearchTerm.Substring(index + 1); // skip space
                        options.Where = b => b.BookAuthors.Any(
                            ba => ba.Author.FirstName.Contains(first) &&  // AND
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

        // add
        [HttpGet]
        public ViewResult Add(int id) => GetBook(id, "Add");

        [HttpPost]
        public IActionResult Add(BookViewModel vm)
        {
            if (ModelState.IsValid) {
                data.AddNewBookAuthors(vm.Book, vm.SelectedAuthors);
                data.Books.Insert(vm.Book);
                data.Save();

                TempData["message"] = $"{vm.Book.Title} added to Books.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else {
                Load(vm, "Add");
                return View("Book", vm);
            }
        }

        // edit
        [HttpGet]
        public ViewResult Edit(int id) => GetBook(id, "Edit");
        
        [HttpPost]
        public IActionResult Edit(BookViewModel vm)
        {
            if (ModelState.IsValid) {
                data.DeleteCurrentBookAuthors(vm.Book);
                data.AddNewBookAuthors(vm.Book, vm.SelectedAuthors);
                data.Books.Update(vm.Book);
                data.Save(); 
                
                TempData["message"] = $"{vm.Book.Title} updated.";
                return RedirectToAction("Search");  // PRG pattern
            }
            else {
                Load(vm, "Edit");
                return View("Book", vm);
            }
        }

        // delete
        [HttpGet]
        public ViewResult Delete(int id) => GetBook(id, "Delete");

        [HttpPost]
        public IActionResult Delete(BookViewModel vm)
        {
            // no ModelState.IsValid check here bc there's no user input - 
            // only BookId in hidden field is posted from form. 
            data.Books.Delete(vm.Book); // cascading delete will get BookAuthors
            data.Save();
            TempData["message"] = $"{vm.Book.Title} removed from Books.";
            return RedirectToAction("Search");  // PRG pattern
        }

        // private helper methods
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
                    Includes = "BookAuthors.Author, Genre",
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