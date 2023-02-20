using System.Collections.Generic;

namespace Bookstore.Models
{
    public class AuthorListViewModel
    {
        public IEnumerable<Author> Authors { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}
