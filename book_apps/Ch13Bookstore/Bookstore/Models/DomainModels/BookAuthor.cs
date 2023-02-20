namespace Bookstore.Models
{
    public class BookAuthor
    {
        // composite primary key and foreign keys
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        // navigation properties
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
