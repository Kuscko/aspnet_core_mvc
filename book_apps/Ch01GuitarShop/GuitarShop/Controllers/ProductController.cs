using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GuitarShop.Models;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Detail(string id)
        {
            Product product = DB.GetProduct(id);
            return View(product);
        }

        public IActionResult List()
        {
            List<Product> products = DB.GetProducts();
            return View(products);
        }
    }
}