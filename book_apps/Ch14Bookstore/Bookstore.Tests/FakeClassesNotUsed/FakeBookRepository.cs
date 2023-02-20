using Bookstore.Models;
using System;
using System.Collections.Generic;

namespace Bookstore.Tests
{
    class FakeBookRepository : IRepository<Book>
    {
        public int Count => throw new NotImplementedException();
        public void Delete(Book entity) => throw new NotImplementedException();
        public Book Get(QueryOptions<Book> options) => new Book();  // returns a new Book object

        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public Book Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Book entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> List(QueryOptions<Book> options)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
