using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bookstore.Models
{
    public class QueryOptions<T>
    {
        // public properties for sorting, filtering, and paging
        public Expression<Func<T, Object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc";  // default
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // filter by more than one where clause. User can set value for Where property 
        // repeatedly, or can instantiate a new WhereClauses property, whose type
        // is a class that inherits a list of where expressions (see below)
        public WhereClauses<T> WhereClauses { get; set; }
        public Expression<Func<T, bool>> Where {
            set {
                if (WhereClauses == null)
                {
                    WhereClauses = new WhereClauses<T>();
                }
                WhereClauses.Add(value);
            }
        }

        // private backing field for property and method that work with Include strings
        private string[] includes;

        // public write-only property for Include strings – converts comma-separated string to array 
        // and stores in private backing field
        public string Includes {
            set => includes = value.Replace(" ", "").Split(',');
        }

        // public get method for Include strings - returns private backing field or empty string array 
        // if private backing field is null
        public string[] GetIncludes() => includes ?? new string[0];

        // read-only properties 
        public bool HasWhere => WhereClauses != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }

    // basically an alias for a list of where expressions - to make code clearer
    public class WhereClauses<T>:List<Expression<Func<T, bool>>> {}

}
