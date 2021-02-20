using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teaching.Certification.OA.AspNetMvc.Controllers
{
    using Data;

    [Authorize(Roles = DbContextPopulator.RoleNameAdministrator)]
    public class DocumentController : Controller
    {
        private readonly IStore<Document> _documentStore;
        private readonly IStore<DocumentCategory> _categoryStore;
        private readonly IStore<User> _userStore;


        public DocumentController(IStore<Document> documentStore,
            IStore<DocumentCategory> categoryStore,
            IStore<User> userStore)
        {
            _documentStore = documentStore;
            _categoryStore = categoryStore;
            _userStore = userStore;
        }


        public IActionResult Index(int? page, int? size)
        {
            ViewBag.CategoryStore = _categoryStore;
            ViewBag.UserStore = _userStore;

            return View(_documentStore.GetPagingDocuments(DocumentStatus.Normal, key: null, page, size));
        }


        public IActionResult Recycle(int? page, int? size)
        {
            ViewBag.CategoryStore = _categoryStore;
            ViewBag.UserStore = _userStore;

            return View(_documentStore.GetPagingDocuments(DocumentStatus.Deleted, key: null, page, size));
        }


        public IActionResult Search(int? page, int? size, string key = null)
        {
            ViewBag.Key = key;
            ViewBag.CategoryStore = _categoryStore;
            ViewBag.UserStore = _userStore;

            return View(_documentStore.GetPagingDocuments(status: null, key, page, size));
        }

    }
}
