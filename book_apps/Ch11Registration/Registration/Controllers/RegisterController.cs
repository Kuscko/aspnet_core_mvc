using System;
using Microsoft.AspNetCore.Mvc;
using Registration.Models;

namespace Registration.Controllers
{
    public class RegisterController : Controller
    {
        private RegistrationContext context;
        public RegisterController(RegistrationContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(Customer customer)
        {
            if (TempData["okEmail"] == null)
            {
                string msg = Check.EmailExists(context, customer.EmailAddress);
                if (!String.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(Customer.EmailAddress), msg);
                }
            }

            if (ModelState.IsValid)
            {
                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Welcome");
            }
            else
            {
                return View(customer);
            }
        }

        [HttpGet]
        public IActionResult Welcome(Customer customer)
        {
            return View();
        }
    }
}