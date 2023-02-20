using System;
using System.Collections.Generic;
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

            // if no count value in session, use data in cookie to restore fave teams in session 
            int? count = session.GetMyTeamCount();
            if (count == null) {
                var cookies = new NFLCookies(HttpContext.Request.Cookies);
                string[] ids = cookies.GetMyTeamIds();

                List<Team> myteams = new List<Team>(); 
                if (ids.Length > 0)
                    myteams = context.Teams.Include(t => t.Conference)
                        .Include(t => t.Division)
                        .Where(t => ids.Contains(t.TeamID)).ToList();
                session.SetMyTeams(myteams);
            }

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
        public RedirectToActionResult Add(TeamViewModel model)
        {
            model.Team = context.Teams
                .Include(t => t.Conference)
                .Include(t => t.Division)
                .Where(t => t.TeamID == model.Team.TeamID)
                .FirstOrDefault();

            var session = new NFLSession(HttpContext.Session);
            var teams = session.GetMyTeams();
            teams.Add(model.Team);
            session.SetMyTeams(teams);

            var cookies = new NFLCookies(HttpContext.Response.Cookies);
            cookies.SetMyTeamIds(teams);

            TempData["message"] = $"{model.Team.Name} added to your favorites";

            return RedirectToAction("Index",
                new {
                    ActiveConf = session.GetActiveConf(),
                    ActiveDiv = session.GetActiveDiv()
                });
        }
    }
}