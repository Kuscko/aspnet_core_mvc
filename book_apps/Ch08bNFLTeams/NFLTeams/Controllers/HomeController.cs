using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFLTeams.Models;

namespace NFLTeams.Controllers
{
    public class HomeController : Controller
    {
        private TeamContext context;

        public HomeController(TeamContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index(string activeConf = "all", 
                                   string activeDiv = "all")
        {
            var data = new TeamListViewModel
            {
                ActiveConf = activeConf,
                ActiveDiv = activeDiv,
                Conferences = context.Conferences.ToList(),
                Divisions = context.Divisions.ToList()
            };

            IQueryable<Team> query = context.Teams;
            if (activeConf != "all")
                query = query.Where(
                    t => t.Conference.ConferenceID.ToLower() == activeConf.ToLower());
            if (activeDiv != "all")
                query = query.Where(
                    t => t.Division.DivisionID.ToLower() == activeDiv.ToLower());
            data.Teams = query.ToList();

            return View(data);
        }

        [HttpPost]
        public IActionResult Details(TeamViewModel model)
        {
            Utility.LogTeamClick(model.Team.TeamID);

            TempData["ActiveConf"] = model.ActiveConf;
            TempData["ActiveDiv"] = model.ActiveDiv;
            return RedirectToAction("Details", new { ID = model.Team.TeamID });
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = new TeamViewModel
            {
                Team = context.Teams
                    .Include(t => t.Conference)
                    .Include(t => t.Division)
                    .FirstOrDefault(t => t.TeamID == id),
                ActiveDiv = TempData?["ActiveDiv"]?.ToString() ?? "all",
                ActiveConf = TempData?["ActiveConf"]?.ToString() ?? "all"
            };
            return View(model);
        }
    }
}