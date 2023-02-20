using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Home controller, Index action");
        }

        public IActionResult About()
        {
            return Content("Home controller, About action");
        }
    }
}