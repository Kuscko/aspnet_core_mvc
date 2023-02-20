using Microsoft.AspNetCore.Mvc;
using Registration.Models;

namespace Registration.Controllers
{
    public class ValidationController : Controller
    {
        private RegistrationContext context;
        public ValidationController(RegistrationContext ctx) => context = ctx;

        public JsonResult CheckEmail(string emailAddress)
        {
            string msg = Check.EmailExists(context, emailAddress);
            if (string.IsNullOrEmpty(msg)) {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
    }
}