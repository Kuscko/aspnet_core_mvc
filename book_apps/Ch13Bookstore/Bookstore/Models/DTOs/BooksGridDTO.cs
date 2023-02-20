using Newtonsoft.Json;

namespace Bookstore.Models
{
    // Inherits the general purpose GridDTO class and adds bookstore-specific 
    // properties for the filtering route segments defined in the Startup.cs file. 

    // Instances of this class are stored in session after being converted to a 
    // JSON string. Since the readonly DefaultFilter property doesn't need
    // to be stored, it's decorated with the [JsonIgnore] attribute so it will 
    // be skipped when the JSON string is created.

    public class BooksGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Author { get; set; } = DefaultFilter;
        public string Genre { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
    }
}
