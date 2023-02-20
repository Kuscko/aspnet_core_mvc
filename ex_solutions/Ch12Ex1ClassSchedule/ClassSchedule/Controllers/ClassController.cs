using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class ClassController : Controller
    {
        private ClassScheduleUnitOfWork data { get; set; }
        public ClassController(ClassScheduleContext ctx) => data = new ClassScheduleUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            this.LoadViewBag("Add");
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            this.LoadViewBag("Edit");
            var c = this.GetClass(id);
            return View("Add", c);
        }

        [HttpPost]
        public IActionResult Add(Class c)
        {
            if (ModelState.IsValid) {
                if (c.ClassId == 0)
                    data.Classes.Insert(c);
                else
                    data.Classes.Update(c);
                data.Classes.Save();
                return RedirectToAction("Index", "Home");
            }
            else {
                string operation = (c.ClassId == 0) ? "Add" : "Edit";
                this.LoadViewBag(operation);
                return View();
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var c = this.GetClass(id);
            return View(c);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Class c)
        {
            data.Classes.Delete(c);
            data.Classes.Save();
            return RedirectToAction("Index", "Home");
        }

        // private helper methods
        private Class GetClass(int id)
        {
            var classOptions = new QueryOptions<Class> {
                Includes = "Teacher, Day",
                Where = c => c.ClassId == id
            };
            return data.Classes.Get(classOptions);
        }

        private void LoadViewBag(string operation)
        {
            ViewBag.Days = data.Days.List(new QueryOptions<Day> {
                OrderBy = d => d.DayId
            });
            ViewBag.Teachers = data.Teachers.List(new QueryOptions<Teacher> {
                OrderBy = t => t.LastName
            });
            ViewBag.Operation = operation;
        }
    }
}