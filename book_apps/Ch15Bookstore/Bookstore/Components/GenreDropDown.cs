using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Components
{
    public class GenreDropDown : ViewComponent
    {
        private IRepository<Genre> data { get; set; }
        public GenreDropDown(IRepository<Genre> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var genres = data.List(new QueryOptions<Genre> {
                OrderBy = g => g.Name
            });
            
            var vm = new DropDownViewModel {
                SelectedValue = selectedValue,
                DefaultValue = BooksGridDTO.DefaultFilter,
                Items = genres.ToDictionary(g => g.GenreId.ToString(), g => g.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
