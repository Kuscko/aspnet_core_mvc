using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private Repository<Book> data { get; set; }
        public CartController(BookstoreContext ctx) => data = new Repository<Book>(ctx);


        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
            return cart;
        }

        public ViewResult Index() 
        {
            var cart = GetCart();
            var builder = new BooksGridBuilder(HttpContext.Session);

            var vm = new CartViewModel {
                List = cart.List,
                Subtotal = cart.Subtotal,
                BookGridRoute = builder.CurrentRoute
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var book = data.Get(new QueryOptions<Book> {
                Include = "BookAuthors.Author, Genre",
                Where = b => b.BookId == id
            });
            if (book == null){
                TempData["message"] = "Unable to add book to cart.";   
            }
            else {
                var dto = new BookDTO();
                dto.Load(book);
                CartItem item = new CartItem {
                    Book = dto,
                    Quantity = 1  
                };

                Cart cart = GetCart();
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{book.Title} added to cart";
            }

            var builder = new BooksGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Book", builder.CurrentRoute);
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Book.Title} removed from cart.";
            return RedirectToAction("Index");
        }
                
        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Cart cart = GetCart();
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            Cart cart = GetCart();
            cart.Edit(item);
            cart.Save();

            TempData["message"] = $"{item.Book.Title} updated";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}