using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Models
{
    public class Genre
    {
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter a genre id.")]
        [Remote("CheckGenre", "Validation", "Admin")] // include 'Admin' area name
        public string GenreId { get; set; }
        
        [StringLength(25)]
        [Required(ErrorMessage = "Please enter a genre name.")]
        public string Name { get; set; }

        // navigation property
        public ICollection<Book> Books { get; set; }
    }
}