using System.Linq;

namespace Bookstore.Models
{
    public class BookstoreUnitOfWork : IBookstoreUnitOfWork
    {
        private BookstoreContext context { get; set; }
        public BookstoreUnitOfWork(BookstoreContext ctx) => context = ctx;

        private Repository<Book> bookData;
        public Repository<Book> Books {
            get {
                if (bookData == null)
                    bookData = new Repository<Book>(context);
                return bookData;
            }
        }

        private Repository<Author> authorData;
        public Repository<Author> Authors {
            get {
                if (authorData == null)
                    authorData = new Repository<Author>(context);
                return authorData;
            }
        }

        private Repository<BookAuthor> bookauthorData;
        public Repository<BookAuthor> BookAuthors {
            get {
                if (bookauthorData == null)
                    bookauthorData = new Repository<BookAuthor>(context);
                return bookauthorData;
            }
        }

        private Repository<Genre> genreData;
        public Repository<Genre> Genres
        {
            get {
                if (genreData == null)
                    genreData = new Repository<Genre>(context);
                return genreData;
            }
        }

        public void DeleteCurrentBookAuthors(Book book)
        {
            var currentAuthors = BookAuthors.List(new QueryOptions<BookAuthor> {
                Where = ba => ba.BookId == book.BookId
            });
            foreach (BookAuthor ba in currentAuthors) {
                BookAuthors.Delete(ba);
            }
        }

        public void LoadNewBookAuthors(Book book, int[] authorids)
        {
            book.BookAuthors = authorids.Select(i =>
                new BookAuthor { Book = book, AuthorId = i })
                .ToList();
        }

        public void Save() => context.SaveChanges();
    }
}