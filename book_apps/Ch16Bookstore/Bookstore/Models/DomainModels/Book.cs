using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public partial class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        [StringLength(200)]
        public string Title { get; set; }

        [Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please select a genre.")]
        public string GenreId { get; set; }

        public Genre Genre { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
