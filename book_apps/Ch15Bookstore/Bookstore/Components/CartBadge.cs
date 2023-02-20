using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Components
{
    public class CartBadge : ViewComponent
    {
        private ICart cart { get; set; }
        public CartBadge(ICart c) => cart = c;

        public IViewComponentResult Invoke()
        {
            return View(cart.Count);
        }
    }
}
