
namespace Bookstore.Models
{
    // General purpose class for model binding the paging and sorting route segments
    // defined in the Startup.cs file - can be used for any web application. Doesn't
    // include any filter properties, as those are usually application-specific. 

    public class GridDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public string SortField { get; set; }
        public string SortDirection { get; set; } = "asc";
    }
}
