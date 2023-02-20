using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}