using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class TeacherController : Controller
    {
        private IRepository<Teacher> teachers { get; set; }
        public TeacherController(IRepository<Teacher> rep) => teachers = rep;

        public ViewResult Index()
        {
            var options = new QueryOptions<Teacher> {
                OrderBy = t => t.LastName
            };
            return View(teachers.List(options));
        }

        [HttpGet]
        public ViewResult Add() => View();

        [HttpPost]
        public IActionResult Add(Teacher teacher)
        {
            if (ModelState.IsValid) {
                teachers.Insert(teacher);
                teachers.Save();
                return RedirectToAction("Index");
            }
            else{
                return View(teacher);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            return View(teachers.Get(id));
        }

        [HttpPost]
        public RedirectToActionResult Delete(Teacher teacher)
        {
            teachers.Delete(teacher);
            teachers.Save();
            return RedirectToAction("Index");
        }
    }
}