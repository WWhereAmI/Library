using Library.Services;
using Library.ViewModels.BookAuditVm;
using System;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BookAuditController : Controller
    {
        private readonly BookAuditReadService _bookAuditReadService;
        private readonly BookAuditEditService _bookAuditEditService;

        public BookAuditController(BookAuditReadService bookAuditReadService, BookAuditEditService bookAuditEditService)
        {
            _bookAuditReadService = bookAuditReadService;
            _bookAuditEditService = bookAuditEditService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBookAudits()
        {
            var bookAudits = _bookAuditReadService.GetAll();
            return Json(bookAudits, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BookIssuance()
        {
            var vm = _bookAuditReadService.GetBookIssuanceVm();

            ViewBag.Action = "PostBookIssuance";

            return PartialView("BookTransition", vm);
        }

        [HttpPost]
        public ActionResult PostBookIssuance(BookTransitionVm vm)
        {
            if (ModelState.IsValid)
            {
                _bookAuditEditService.IssuanceBook(vm);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult BookReturn()
        {
            var vm = _bookAuditReadService.GetBookReturnVm();

            ViewBag.Action = "PostBookReturn";

            return PartialView("BookTransition", vm);
        }

        [HttpPost]
        public ActionResult PostBookReturn(BookTransitionVm vm)
        {
            if (ModelState.IsValid)
            {
                _bookAuditEditService.ReturnBook(vm);
            }

            return RedirectToAction("Index");
        }

    }
}