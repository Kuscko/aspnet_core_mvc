using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GuitarShop.Models;

namespace GuitarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ShopContext context;

        public CategoryController(ShopContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Route("[area]/Categories/{id?}")]
        public IActionResult List()
        {
            var categories = context.Categories
                .OrderBy(c => c.CategoryID).ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddUpdate", new Category());
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Action = "Update";
            var category = context.Categories.Find(id);
            return View("AddUpdate", category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryID == 0)
                {
                    context.Categories.Add(category);
                }
                else
                {
                    context.Categories.Update(category);
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Action = "Save";
                return View("AddUpdate");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Category category = context.Categories.Find(id);
            return View(category);
        }


        [HttpPost]
        public IActionResult Delete(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}