using Library.DataAccess;
using Library.Models;
using Library.Services.BaseServices;
using Library.ViewModels.BookAuditVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Services
{
    public class BookAuditReadService : BaseReadService<BookAudit>
    {
        private readonly IRepository<BookAudit> _bookAuditRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Book> _bookRepository;

        public BookAuditReadService(IRepository<BookAudit> bookAuditRepository,
            IRepository<Client> clientRepository,
            IRepository<Book> bookRepository) :base(bookAuditRepository)
        {
            _bookAuditRepository = bookAuditRepository;
            _clientRepository = clientRepository;
            _bookRepository = bookRepository;
        }

        public List<BookAudit> GetAll()
        {
            return _bookAuditRepository.GetAll().ToList();

        }

        public BookTransitionVm GetBookIssuanceVm()
        {
            return new BookTransitionVm
            {
                ClientSelectList = _clientRepository.GetAll().Select(e => new SelectListItem()
                {
                    Text = e.Name + " " +  e.Family,
                    Value = e.Id.ToString(),
                    Selected = true
                }),

                BookSelectList = _bookRepository.GetAll().Where(e => e.Status == Const.BookStatus.InStock).Select(e => new SelectListItem()
                {
                    Text = e.Id + "." + e.Title,
                    Value = e.Id.ToString(),
                    Selected = true
                })
            };
        }

        public BookTransitionVm GetBookReturnVm()
        {
            return new BookTransitionVm
            {
                BookSelectList = _bookAuditRepository.GetAll().Select(e => new SelectListItem()
                {
                    Text = e.BookId + "." + e.Book,
                    Value = e.Id.ToString(),
                    Selected = true
                })
            };
        }
    }
}