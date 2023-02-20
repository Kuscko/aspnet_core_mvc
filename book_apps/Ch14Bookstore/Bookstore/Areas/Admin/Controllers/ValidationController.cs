using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        public JsonResult CheckGenre(string genreId,
            [FromServices] IRepository<Genre> data)
        {
            var validate = new Validate(TempData);
            validate.CheckGenre(genreId, data);
            if (validate.IsValid) {
                validate.MarkGenreChecked();
                return Json(true);
            }
            else {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckAuthor(string firstName, string lastName, 
            string operation, [FromServices] IRepository<Author> data)
        {
            var validate = new Validate(TempData);
            validate.CheckAuthor(firstName, lastName, operation, data);
            if (validate.IsValid) {
                validate.MarkAuthorChecked();
                return Json(true);
            }
            else {
                return Json(validate.ErrorMessage);
            }
        }

    }
}