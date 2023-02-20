using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Components
{
    public class AuthorDropDown : ViewComponent
    {
        private IRepository<Author> data { get; set; }
        public AuthorDropDown(IRepository<Author> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var authors = data.List(new QueryOptions<Author> {
                OrderBy = a => a.FirstName
            });
            
            var vm = new DropDownViewModel {
                SelectedValue = selectedValue,
                DefaultValue = BooksGridDTO.DefaultFilter,
                Items = authors.ToDictionary(
                    a => a.AuthorId.ToString(), a => a.FullName)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
