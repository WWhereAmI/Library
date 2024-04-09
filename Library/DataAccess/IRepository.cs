using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DataAccess
{
    interface IRepository<T> : IDisposable 
        where T : class
    {
        IEnumerable<T> GetAll();
        T Find(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Save();
    }
}