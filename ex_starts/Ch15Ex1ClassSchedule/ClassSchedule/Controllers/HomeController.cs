using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class HomeController : Controller
    {
        private IClassScheduleUnitOfWork data { get; set; }
        public HomeController(IClassScheduleUnitOfWork unit) => data = unit;

        public ViewResult Index(int id)
        {
            // options for Days query
            var dayOptions = new QueryOptions<Day> { 
                OrderBy = d => d.DayId
            };
            ViewBag.Days = data.Days.List(dayOptions);

            // options for Classes query
            var classOptions = new QueryOptions<Class> {
                Includes = "Teacher, Day"
            };
            // order by day if no filter. Otherwise, filter by day and order by time.
            if (id == 0) {
                classOptions.OrderBy = c => c.DayId;
            }
            else {
                classOptions.Where = c => c.DayId == id;
                classOptions.OrderBy = c => c.MilitaryTime;
            }

            // execute queries
            return View(data.Classes.List(classOptions));
        }
    }
}