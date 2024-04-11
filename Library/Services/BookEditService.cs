using Library.DataAccess;
using Library.Models;
using Library.Services.BaseServices;

namespace Library.Services
{
    public class BookEditService : BaseEditService<Book>
    {
        public BookEditService(IRepository<Book> repository) : base(repository)
        {
        }
    }
}