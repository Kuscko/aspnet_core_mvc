using Bookstore.Models;
using System;
using System.Collections.Generic;

namespace Bookstore.Tests
{
    class FakeAuthorRepository : IRepository<Author>
    {
        public void Update(Author entity) { }  // does nothing
        public void Save() { }  // does nothing

        public int Count => throw new NotImplementedException();

        public void Delete(Author entity)
        {
            throw new NotImplementedException();
        }

        public Author Get(QueryOptions<Author> options)
        {
            throw new NotImplementedException();
        }

        public Author Get(int id)
        {
            throw new NotImplementedException();
        }

        public Author Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Author entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> List(QueryOptions<Author> options)
        {
            throw new NotImplementedException();
        }
    }
}
