 using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
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
            // create a new Cart object and get items from session or restore from cookie and db
            Cart cart = GetCart();

            // create a new builder object to work with route data in session
            var builder = new BooksGridBuilder(HttpContext.Session);

            // create a new view model object with cart and route information and pass it to the view
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
            // get the book the user chose from the database
            var book = data.Get(new QueryOptions<Book> {
                Includes = "BookAuthors.Author, Genre",
                Where = b => b.BookId == id
            });
            if (book == null){
                TempData["message"] = "Unable to add book to cart.";   
            }
            else {
                // create and load a new Book DTO, then add it to a new CartItem object
                // with a default quantity of one.
                var dto = new BookDTO();
                dto.Load(book);
                CartItem item = new CartItem {
                    Book = dto,
                    Quantity = 1  // default value
                };

                // add new item to cart and save to session state
                Cart cart = GetCart();
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{book.Title} added to cart";
            }

            // create a new builder object to work with route data in session, then 
            // redirect to Book/List action method, passing a dictionary of route segment 
            // values to build URL 
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
            // get selected cart item from session and pass it to the view
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