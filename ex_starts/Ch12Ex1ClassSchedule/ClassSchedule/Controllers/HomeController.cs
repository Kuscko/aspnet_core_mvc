using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Class> classes { get; set; }
        private Repository<Day> days { get; set; }

        public HomeController(ClassScheduleContext ctx) {
            classes = new Repository<Class>(ctx);
            days = new Repository<Day>(ctx);
        }

        public ViewResult Index(int id)
        {
            // options for Days query
            var dayOptions = new QueryOptions<Day> { 
                OrderBy = d => d.DayId
            };

            // options for Classes query
            var classOptions = new QueryOptions<Class> {
                Includes = "Teacher, Day"
            };
            // order by Day if no filter. Otherwise, filter by day and order by time.
            if (id == 0) {
                classOptions.OrderBy = c => c.DayId;
            }
            else {
                classOptions.Where = c => c.DayId == id;
                classOptions.OrderBy = c => c.MilitaryTime;
            }

            // execute queries
            ViewBag.Days = days.List(dayOptions);
            return View(classes.List(classOptions));
        }
    }
}
