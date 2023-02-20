using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected BookstoreContext context { get; set; }
        private DbSet<T> dbset { get; set; }
        public Repository(BookstoreContext ctx) {
            context = ctx;
            dbset = context.Set<T>();
        }

        // get number of retrieved entities - use private backing field bc when 
        // filtering, count might be less than dbset.Count()
        private int? count;
        public int Count => count ?? dbset.Count();

        // retrieve a list of entities
        public virtual IEnumerable<T> List(QueryOptions<T> options) {
            IQueryable<T> query = BuildQuery(options);
            return query.ToList();
        }

        // retrieve a single entity (3 overloads)
        public virtual T Get(int id) => dbset.Find(id);
        public virtual T Get(string id) => dbset.Find(id);
        public virtual T Get(QueryOptions<T> options) {
            IQueryable<T> query = BuildQuery(options);
            return query.FirstOrDefault();
        }

        // insert, update, delete, save
        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => context.SaveChanges();

        // private helper method to build query expression
        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.GetIncludes()) {
                query = query.Include(include);
            }
            if (options.HasWhere) { 
                foreach (var clause in options.WhereClauses) {
                    query = query.Where(clause);
                }
                count = query.Count(); // get filtered count
            }
            if (options.HasOrderBy){
                if (options.OrderByDirection == "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }
            if (options.HasPaging)
                query = query.PageBy(options.PageNumber, options.PageSize);

            return query;
        }
    }
}
