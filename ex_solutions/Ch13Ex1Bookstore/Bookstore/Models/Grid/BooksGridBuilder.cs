using Microsoft.AspNetCore.Http;

namespace Bookstore.Models
{
    // inherits the general purpose GridBuilder class and adds application-specific 
    // methods for loading and clearing filter route segments in route dictionary.
    // Also adds application-specific Boolean flags for sorting and filtering. 

    public class BooksGridBuilder : GridBuilder
    {
        // this constructor gets current route data from session
        public BooksGridBuilder(ISession sess) : base(sess) { }

        // this constructor stores filtering route segments, as well as
        // the paging and sorting route segments stored by the base constructor
        public BooksGridBuilder(ISession sess, BooksGridDTO values, 
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            // store filter route segments - add filter prefixes if this is initial load
            // of page with default values rather than route values (route values have prefix)
            bool isInitial = values.Genre.IndexOf(FilterPrefix.Genre) == -1;
            routes.AuthorFilter = (isInitial) ? FilterPrefix.Author + values.Author : values.Author;
            routes.GenreFilter = (isInitial) ? FilterPrefix.Genre + values.Genre : values.Genre;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;

            SaveRouteSegments();
        }

        // load new filter route segments contained in a string array - add filter prefix 
        // to each one. if filtering by author (rather than just 'all'), add author slug 
        public void LoadFilterSegments(string[] filter, Author author)
        {
            if (author == null) { 
                routes.AuthorFilter = FilterPrefix.Author + filter[0];
            } else {
                routes.AuthorFilter = FilterPrefix.Author + filter[0]
                    + "-" + author.FullName.Slug();
            }
            routes.GenreFilter = FilterPrefix.Genre + filter[1];
            routes.PriceFilter = FilterPrefix.Price + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        //~~ filter flags ~~//
        string def = BooksGridDTO.DefaultFilter;   // get default filter value from static DTO property
        public bool IsFilterByAuthor => routes.AuthorFilter != def;
        public bool IsFilterByGenre => routes.GenreFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        //~~ sort flags ~~//
        public bool IsSortByGenre =>
            routes.SortField.EqualsNoCase(nameof(Genre));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Book.Price));
    }
}
