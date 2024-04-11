using Library.Services;
using Library.ViewModels.BookVm;
using Library.ViewModels.ClientVm;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientReadService _clientReadService;
        private readonly ClientEditService _clientEdiService;

        public ClientController(ClientReadService clientReadService, ClientEditService clientEdiService)
        {
            _clientReadService = clientReadService;
            _clientEdiService = clientEdiService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetClients()
        {
            var clients = _clientReadService.GetAll();
            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddClient(ClientAddVm vm)
        {
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult PostAdd(ClientAddVm vm)
        {
            if (ModelState.IsValid)
            {
                _clientEdiService.Add(vm);
            }

            return RedirectToAction("Index");
        }
    }
}