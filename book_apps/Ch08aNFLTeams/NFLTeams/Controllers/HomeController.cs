using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        public ViewResult Index(string activeConf = "all", string activeDiv= "all")
        {
            // store selected conference and division IDs in view bag
            ViewBag.ActiveConf = activeConf;
            ViewBag.ActiveDiv = activeDiv;

            // get list of conferences and divisions from database
            List<Conference> conferences = context.Conferences.ToList();
            List<Division> divisions = context.Divisions.ToList();

            // insert default "All" value at front of each list
            conferences.Insert(0, new Conference { ConferenceID = "all", Name = "All" });
            divisions.Insert(0, new Division { DivisionID = "all", Name = "All" });

            // store lists in view bag
            ViewBag.Conferences = conferences;
            ViewBag.Divisions = divisions;

            // get teams - filter by conference and division
            IQueryable<Team> query = context.Teams;
            if (activeConf != "all")
                query = query.Where(
                    t => t.Conference.ConferenceID.ToLower() == activeConf.ToLower());
            if (activeDiv != "all")
                query = query.Where(
                    t => t.Division.DivisionID.ToLower() == activeDiv.ToLower());

            // pass teams to view as model
            var teams = query.ToList();
            return View(teams);
        }
    }
}