using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TempManager.Models;

namespace Ch11Ex1TempManager.Controllers
{
    public class HomeController : Controller
    {
        private TempManagerContext data { get; set; }
        public HomeController(TempManagerContext ctx) => data = ctx;

        public ViewResult Index()
        {
            var temps = data.Temps.OrderBy(t => t.Date).ToList();
            return View(temps);
        }

        [HttpGet]
        public ViewResult Add() => View(new Temp());

        [HttpPost]
        public IActionResult Add(Temp temp)
        {
            // server-side check for remote validation for duplicate date
            Temp check = data.Temps.FirstOrDefault(t => t.Date == temp.Date);
            if (check != null) { 
                ModelState.AddModelError("Date",
                    $"The date {temp.Date?.ToShortDateString()} is already in the database.");
            }

            if (ModelState.IsValid) {
                data.Temps.Add(temp);
                data.SaveChanges();

                return RedirectToAction("Index");
            } 
            else {
                // model-level validation message
                ModelState.AddModelError("", "Please correct all errors.");
                return View(temp);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var temp = data.Temps.Find(id);
            return View(temp);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Temp temp)
        {
            data.Remove(temp);
            data.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}