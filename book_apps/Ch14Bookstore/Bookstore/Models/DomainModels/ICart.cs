using System.Collections.Generic;

namespace Bookstore.Models
{
    public interface ICart
    {
        void Load(IRepository<Book> data);
        double Subtotal { get; }
        int? Count { get; }
        IEnumerable<CartItem> List { get; }
        CartItem GetById(int id);
        void Add(CartItem item);
        void Edit(CartItem item);
        void Remove(CartItem item);
        void Clear();
        void Save();
    }
}