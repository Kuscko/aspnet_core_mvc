using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [MaxLength(200)]
        [Remote("CheckAuthor", "Validation", "Area", 
            AdditionalFields = "FirstName, Operation")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
