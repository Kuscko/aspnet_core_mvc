using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models
{
    // static class of constants used to add and remove user-friendly
    // prefix from filter route segment values. Public class rather
    // than private constants bc also used by BookstoreGridBuilder class.

    public static class FilterPrefix
    {
        public const string Genre = "genre-";
        public const string Price = "price-";
        public const string Author = "author-";
    }

    // inherits dictionary of strings, adds a Clone() method. Adds properties
    // to get and set general paging, sorting, and filtering values from dictionary. 
    // Adds methods to set sort field value and sort direction value based on sort field, re-set filter values.

    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        public void SetSortAndDirection(string fieldName, RouteDictionary current) {
            this[nameof(GridDTO.SortField)] = fieldName;

            // set sort direction based on comparison of new and current sort field. if 
            // sort field is same as current, toggle between ascending and descending. 
            // if it's different, should always be ascending.
            if (current.SortField.EqualsNoCase(fieldName) && 
                current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }

        public string GenreFilter {
            get => Get(nameof(BooksGridDTO.Genre))?.Replace(FilterPrefix.Genre, "");
            set => this[nameof(BooksGridDTO.Genre)] = value;
        }

        public string PriceFilter {
            get => Get(nameof(BooksGridDTO.Price))?.Replace(FilterPrefix.Price, "");
            set => this[nameof(BooksGridDTO.Price)] = value;
        }

        public string AuthorFilter {
            get
            {
                // author filter contains prefix, author id, and slug (eg, author-8-ta-nehisi-coates).
                // only need author id for filtering, so first remove 'author-' prefix from string. At
                // that point, the authorid will be at beginning of string. So find index of dash after 
                // id number and then return substring from beginning of string to that index.
                string s = Get(nameof(BooksGridDTO.Author))?.Replace(FilterPrefix.Author, "");
                int index = s?.IndexOf('-') ?? -1;
                return (index == -1) ? s : s.Substring(0, index);
            }
            set => this[nameof(BooksGridDTO.Author)] = value;
        }

        public void ClearFilters() =>
            GenreFilter = PriceFilter = AuthorFilter = BooksGridDTO.DefaultFilter;

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        // return a new dictionary that contains the same values as this dictionary.
        // needed so that pages can change the route values when calculating paging, sorting,
        // and filtering links, without changing the values of the current route
        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys) {
                clone.Add(key, this[key]);
            }
            return clone;
        }
    }
}
