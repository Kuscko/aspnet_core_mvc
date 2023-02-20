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

        public IActionResult Index(string activeConf = "all", string activeDiv = "all")
        {
            var session = new NFLSession(HttpContext.Session);
            session.SetActiveConf(activeConf);
            session.SetActiveDiv(activeDiv);

            var model = new TeamListViewModel
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
            model.Teams = query.ToList();

            return View(model);
        }

        public IActionResult Details(string id)
        {
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamViewModel
            {
                Team = context.Teams
                    .Include(t => t.Conference)
                    .Include(t => t.Division)
                    .FirstOrDefault(t => t.TeamID == id),
                ActiveDiv = session.GetActiveDiv(),
                ActiveConf = session.GetActiveConf()
            };
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Add(TeamViewModel data)
        {
            data.Team = context.Teams
                .Include(t => t.Conference)
                .Include(t => t.Division)
                .Where(t => t.TeamID == data.Team.TeamID)
                .FirstOrDefault();

            var session = new NFLSession(HttpContext.Session);
            var teams = session.GetMyTeams();
            teams.Add(data.Team);
            session.SetMyTeams(teams);

            TempData["message"] = $"{data.Team.Name} added to your favorites";

            return RedirectToAction("Index",
                new {
                    ActiveConf = session.GetActiveConf(),
                    ActiveDiv = session.GetActiveDiv()
                });
        }
    }
}