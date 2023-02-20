using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [MaxLength(200)]
        [Remote("CheckAuthor", "Validation", "Admin", AdditionalFields = "FirstName, Operation")] // include 'Admin' area name
        public string LastName { get; set; }

        // read-only property
        public string FullName => $"{FirstName} {LastName}";

        // navigation property
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
