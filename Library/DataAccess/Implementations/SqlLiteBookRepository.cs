using Library.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DataAccess.Implementations
{
    public class SqlLiteBookRepository : IRepository<Book>
    {
        private readonly SqliteConnection _sqlLiteConnection;

        public SqlLiteBookRepository()
        { 
        }


        public void Add(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _sqlLiteConnection.Dispose();
        }

        public Book Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll()
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