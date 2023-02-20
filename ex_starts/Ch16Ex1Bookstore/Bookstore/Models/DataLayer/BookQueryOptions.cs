using System.Linq;

namespace Bookstore.Models
{
    public class BookQueryOptions : QueryOptions<Book>
    {
        public void SortFilter(BooksGridBuilder builder)
        {
            if (builder.IsFilterByGenre) {
                Where = b => b.GenreId == builder.CurrentRoute.GenreFilter;
            }
            if (builder.IsFilterByPrice) {
                if (builder.CurrentRoute.PriceFilter == "under7")
                    Where = b => b.Price < 7;
                else if (builder.CurrentRoute.PriceFilter == "7to14")
                    Where = b => b.Price >= 7 && b.Price <= 14;
                else
                    Where = b => b.Price > 14;
            }
            if (builder.IsFilterByAuthor) {
                int id = builder.CurrentRoute.AuthorFilter.ToInt();
                if (id > 0)
                    Where = b => b.BookAuthors.Any(ba => ba.Author.AuthorId == id);
            }

            if (builder.IsSortByGenre) {
                OrderBy = b => b.Genre.Name;
            }
            else if (builder.IsSortByPrice) {
                OrderBy = b => b.Price;
            }
            else  {
                OrderBy = b => b.Title;
            }
        }
    }
}
