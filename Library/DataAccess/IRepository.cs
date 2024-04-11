using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DataAccess
{
    public interface IRepository<T> : IDisposable 
        where T : class
    {
        IEnumerable<T> GetAll();
        T Find(int id);

        int Add(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}