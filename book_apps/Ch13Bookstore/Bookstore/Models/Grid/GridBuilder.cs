using Microsoft.AspNetCore.Http;

namespace Bookstore.Models
{
    // general purpose class that holds the values needed to perform paging and sorting. 
    // gets and sets a RouteDictionary object in session. adds methods to load the 
    // RouteDictionary with route segment values. Has properties that hold values needed for paging.

    public class GridBuilder
    {
        protected const string RouteKey = "currentroute";

        protected RouteDictionary routes { get; set; }
        protected ISession session { get; set; }

        // this constructor used when just need to get current route data from session
        public GridBuilder(ISession sess) {
            session = sess;
            routes = session.GetObject<RouteDictionary>(RouteKey) ?? new RouteDictionary();
        }
        // this constructor used when need to store new paging and sorting route segments
        public GridBuilder(ISession sess, GridDTO values, string defaultSortField) {
            session = sess;

            routes = new RouteDictionary(); // clear previous route segment values
            routes.PageNumber = values.PageNumber;
            routes.PageSize = values.PageSize;
            routes.SortField = values.SortField ?? defaultSortField;
            routes.SortDirection = values.SortDirection;

            SaveRouteSegments();
        }

        public void SaveRouteSegments() =>
            session.SetObject<RouteDictionary>(RouteKey, routes);

        public int GetTotalPages(int count) {
            int size = routes.PageSize;
            return (count + size - 1) / size;
        }
            
        public RouteDictionary CurrentRoute => routes;
    }
}
