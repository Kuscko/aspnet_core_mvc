using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class BookViewModel : IValidatableObject
    {
        public Book Book { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public int[] SelectedAuthors { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx) {
            if (SelectedAuthors == null)
            {
                yield return new ValidationResult(
                  "Please select at least one author.",
                  new[] { nameof(SelectedAuthors) });
            }
        }

    }
}
