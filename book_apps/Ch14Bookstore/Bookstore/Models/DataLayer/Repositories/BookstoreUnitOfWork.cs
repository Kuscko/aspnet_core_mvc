namespace Bookstore.Models
{
    public class BookstoreUnitOfWork : IBookstoreUnitOfWork
    {
        private BookstoreContext context { get; set; }
        public BookstoreUnitOfWork(BookstoreContext ctx) => context = ctx;

        private IRepository<Book> bookData;
        public IRepository<Book> Books {
            get {
                if (bookData == null)
                    bookData = new Repository<Book>(context);
                return bookData;
            }
        }

        private IRepository<Author> authorData;
        public IRepository<Author> Authors {
            get {
                if (authorData == null)
                    authorData = new Repository<Author>(context);
                return authorData;
            }
        }

        private IRepository<BookAuthor> bookauthorData;
        public IRepository<BookAuthor> BookAuthors {
            get {
                if (bookauthorData == null)
                    bookauthorData = new Repository<BookAuthor>(context);
                return bookauthorData;
            }
        }

        private IRepository<Genre> genreData;
        public IRepository<Genre> Genres
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

        public void AddNewBookAuthors(Book book, int[] authorids)
        {
            foreach (int id in authorids)
            {
                BookAuthor ba =
                    new BookAuthor { BookId = book.BookId, AuthorId = id };
                BookAuthors.Insert(ba);
            }
        }

        public void Save() => context.SaveChanges();
    }
}