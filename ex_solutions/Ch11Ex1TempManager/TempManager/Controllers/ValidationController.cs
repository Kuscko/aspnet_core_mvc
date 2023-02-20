using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TempManager.Models;

namespace Ch11Ex1TempManager.Controllers
{
    public class ValidationController : Controller
    {
        private TempManagerContext data { get; set; }
        public ValidationController(TempManagerContext ctx) => data = ctx;

        public JsonResult CheckDate(string date)
        {
            DateTime dt = DateTime.Parse(date);
            Temp temp = data.Temps.FirstOrDefault(t => t.Date == dt);

            if (temp == null)
                return Json(true);
            else
                return Json($"The date {date} is already in the database.");
        }
    }
}