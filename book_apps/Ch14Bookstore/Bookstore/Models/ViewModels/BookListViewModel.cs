using System.Collections.Generic;

namespace Bookstore.Models
{
    public class BookListViewModel 
    {
        public IEnumerable<Book> Books { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public Dictionary<string, string> Prices =>
            new Dictionary<string, string> {
                { "under7", "Under $7" },
                { "7to14", "$7 to $14" },
                { "over14", "Over $14" }
            };
    }
}
