using Library.DataAccess;
using Library.Models;
using Library.Services.BaseServices;
using Library.ViewModels.BookAuditVm;
using System;

namespace Library.Services
{
    public class BookAuditEditService : BaseEditService<BookAudit>
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookAudit> _bookAuditRepository;

        public BookAuditEditService(IRepository<BookAudit> repository, IRepository<Book> bookRepository) : base(repository)
        {
            _bookRepository = bookRepository;
            _bookAuditRepository = repository;
        }

        public void IssuanceBook(BookTransitionVm vm)
        {
            ChangeStatus(vm.BookId);

            base.Add(vm);
        }

        public void ReturnBook(BookTransitionVm vm)
        {           
            var bookAudit = _bookAuditRepository.Find(vm.Id);

            ChangeStatus(bookAudit.BookId);

            _bookAuditRepository.Delete(bookAudit.Id);
        }

        private void ChangeStatus(int bookId)
        {
            var book = _bookRepository.Find(bookId);
            book.Status = book.Status == Const.BookStatus.NotAvailable ? Const.BookStatus.InStock : Const.BookStatus.NotAvailable;
            _bookRepository.Update(book);
        }


    }
}