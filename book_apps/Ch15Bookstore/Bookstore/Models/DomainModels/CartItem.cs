using Newtonsoft.Json;

namespace Bookstore.Models
{
    public class CartItem
    {
        public BookDTO Book { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double SubTotal => Book.Price * Quantity;
    }
}
