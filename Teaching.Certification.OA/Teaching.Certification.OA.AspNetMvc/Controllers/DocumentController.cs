using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Teaching.Certification.OA.AspNetMvc.Controllers
{
    using Data;

    [Authorize(Roles = DbContextPopulator.RoleAdministrator)]
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


        public IActionResult Index(int? size, int? page)
        {
            ViewBag.CategoryStore = _categoryStore;
            ViewBag.UserStore = _userStore;

            return View(_documentStore.GetPagingDocuments(DocumentStatus.Normal, key: null, page, size));
        }


        public IActionResult Recycle(int? size, int? page)
        {
            ViewBag.CategoryStore = _categoryStore;
            ViewBag.UserStore = _userStore;

            return View(_documentStore.GetPagingDocuments(DocumentStatus.Deleted, key: null, page, size));
        }


        public IActionResult Search(int? size, int? page, string key = null)
        {
            ViewBag.Key = key;
            ViewBag.CategoryStore = _categoryStore;
            ViewBag.UserStore = _userStore;

            return View(_documentStore.GetPagingDocuments(status: null, key, page, size));
        }

    }
}
