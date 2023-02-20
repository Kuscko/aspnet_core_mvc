using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        [Route("[controller]s/{cat?}")]
        public IActionResult List(string cat = "All")
        {
            return Content("Product controller, List action, Category: " + cat);
        }

        [Route("[controller]/{id}")]
        public IActionResult Detail(int id)
        {
            return Content("Product controller, Detail action, ID: " + id);
        }

        [NonAction]
        public string GetSlug(string name)
        {
            return name.Replace(' ', '-').ToLower();
        }
    }
}