using Microsoft.AspNetCore.Mvc;

using NFLTeams.Models;

namespace NFLTeams.Controllers
{
    public class FavoritesController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamListViewModel
            {
                ActiveConf = session.GetActiveConf(),
                ActiveDiv = session.GetActiveDiv(),
                Teams = session.GetMyTeams()
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Delete()
        {
            var session = new NFLSession(HttpContext.Session);
            var cookies = new NFLCookies(HttpContext.Response.Cookies);

            session.RemoveMyTeams();
            cookies.RemoveMyTeamIds();

            TempData["message"] = "Favorite teams cleared";

            return RedirectToAction("Index", "Home",
                new {
                    ActiveConf = session.GetActiveConf(),
                    ActiveDiv = session.GetActiveDiv()
                });
        }
    }
}