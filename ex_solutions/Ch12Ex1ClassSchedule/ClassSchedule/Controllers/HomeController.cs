using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class HomeController : Controller
    {
        private ClassScheduleUnitOfWork data { get; set; }
        public HomeController(ClassScheduleContext ctx) => data = new ClassScheduleUnitOfWork(ctx);

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
            // order by day and then time if no filter. Otherwise, filter by day and order by time.
            if (id == 0) {
                classOptions.OrderBy = c => c.DayId;
                classOptions.ThenOrderBy = c => c.MilitaryTime;
            }
            else {
                classOptions.Where = c => c.DayId == id;
                classOptions.OrderBy = c => c.MilitaryTime;
            }

            // execute queries
            ViewBag.Days = data.Days.List(dayOptions);
            return View(data.Classes.List(classOptions));
        }
    }
}
