using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class CartController : Controller
    {
        private IRepository<Book> data { get; set; }
        private ICart cart { get; set; }

        public CartController(IRepository<Book> rep, ICart c)
        {
            data = rep;
            cart = c;
            cart.Load(data);
        }

        public ViewResult Index() 
        {
            var vm = new CartViewModel {
                List = cart.List,
                SubTotal = cart.SubTotal
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var book = data.Get(new QueryOptions<Book> {
                Includes = "BookAuthors.Author, Genre",
                Where = b => b.BookId == id
            });
            if (book == null){
                TempData["message"] = "Unable to add book to cart";   
            }
            else {
                var dto = new BookDTO();
                dto.Load(book);
                CartItem item = new CartItem {
                    Book = dto,
                    Quantity = 1  // default value
                };
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{book.Title} added to cart";
            }
            return RedirectToAction("List", "Book");
        }

        public IActionResult Edit(int id)
        {
            CartItem item = cart.GetById(id);
            if (item == null) {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else {
                return View(item);
            }
        }
        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            cart.Edit(item);
            cart.Save();

            TempData["message"] = $"{item.Book.Title} updated";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            CartItem item = cart.GetById(id);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Book.Title} removed from cart.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}