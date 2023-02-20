using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BookAuthor: set primary key 
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });

            // BookAuthor: set foreign keys 
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // Book: remove cascading delete with Genre
            modelBuilder.Entity<Book>().HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .OnDelete(DeleteBehavior.Restrict);

            // seed initial data
            modelBuilder.ApplyConfiguration(new SeedGenres());
            modelBuilder.ApplyConfiguration(new SeedBooks());
            modelBuilder.ApplyConfiguration(new SeedAuthors());
            modelBuilder.ApplyConfiguration(new SeedBookAuthors());
        }
    }
}
