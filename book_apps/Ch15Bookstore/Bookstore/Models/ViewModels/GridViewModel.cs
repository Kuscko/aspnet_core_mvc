using System.Collections.Generic;

namespace Bookstore.Models
{
    public class GridViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}
