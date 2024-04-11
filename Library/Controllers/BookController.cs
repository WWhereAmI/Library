using Library.Services;
using Library.ViewModels.BookVm;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly BookReadService _bookReadService;
        private readonly BookEditService _bookEdiService;

        public BookController(BookReadService bookReadService, BookEditService bookEditService)
        {
            _bookReadService = bookReadService;
            _bookEdiService = bookEditService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBooks()
        {
            var books = _bookReadService.GetAll();
            return Json(books, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddBook(BookAddVm vm)
        {
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult PostAdd(BookAddVm vm)
        {
            if (ModelState.IsValid)
            {
                _bookEdiService.Add(vm);           
            }

            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult EditDescription(int id)
        {
            var vm = _bookReadService.GetEditVm<BookEditDescriptionVm>(id);
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult PutEditDescription(BookEditDescriptionVm vm)
        {

            if (ModelState.IsValid)
            {
                _bookEdiService.Change(vm);
            }           

            return RedirectToAction("Index");
        }
    }
}