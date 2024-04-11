using Library.DataAccess;
using Library.Models;
using Library.Services.BaseServices;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    public class BookReadService : BaseReadService<Book>
    {
        private readonly IRepository<Book> _bookRepository;
        public BookReadService(IRepository<Book> bookRepository) : base(bookRepository) 
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll()
                .ToList();
        }

    }
}