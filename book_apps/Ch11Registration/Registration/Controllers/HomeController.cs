using Microsoft.AspNetCore.Mvc;

namespace Registration.Controllers
{
    public class HomeController : Controller
    {
         public IActionResult Index() => View();
    }
}