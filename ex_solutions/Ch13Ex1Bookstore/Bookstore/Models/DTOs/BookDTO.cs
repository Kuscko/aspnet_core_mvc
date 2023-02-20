using System.Collections.Generic;

namespace Bookstore.Models
{
    // Trying to store a Book object in session can cause problems because the JSON 
    // serialization done in SessionExtensionMethods.cs can create circular references
    // as the serializer tries to follow all the navigation properties. You can decorate
    // those properties with the [JsonIgnore] attribute, but you can end up with that
    // scattered all around. Another way, shown here, is to create a DTO class with the 
    // data needed for the cart. The DTO includes a Load() method to transfer the needed 
    // data from a Book object.

    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public Dictionary<int, string> Authors { get; set; }

        public void Load(Book book)
        {
            BookId = book.BookId;
            Title = book.Title;
            Price = book.Price;
            Authors = new Dictionary<int, string>();
            foreach (BookAuthor ba in book.BookAuthors) {
                Authors.Add(ba.Author.AuthorId, ba.Author.FullName);
            }
        }
    }

}
