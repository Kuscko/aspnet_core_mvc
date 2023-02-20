using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Home controller, Index action");
        }

        [Route("About")]
        public IActionResult About()
        {
            return Content("Home controller, About action");
        }
    }
}