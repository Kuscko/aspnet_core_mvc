using System.Collections.Generic;

namespace Bookstore.Models
{
    public class CartViewModel 
    {
        public IEnumerable<CartItem> List { get; set; }
        public double SubTotal { get; set; }
        public RouteDictionary BookGridRoute { get; set; }
    }
}
