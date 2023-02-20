using System.Linq;
using System.Collections.Generic;

namespace Bookstore.Models
{
    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO {
                BookId = ci.Book.BookId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
