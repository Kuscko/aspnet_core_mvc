using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult List(string cat, int num)
        {
            return Content("Product controller, List action, " +
                "Category " + cat + ", Page " + num);
        }

        public IActionResult Detail(int id)
        {
            return Content("Product controller, Detail action, id: " + id);
        }
    }
}