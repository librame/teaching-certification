using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teaching.Certification.OA.AspNetMvc.Controllers
{
    using Data;

    [Authorize(Roles = DbContextPopulator.RoleAdministrator)]
    public class GlobalController : Controller
    {
        private readonly IStore<Menu> _menuStore;
        private readonly IStore<Role> _roleStore;
        private readonly IStore<User> _userStore;
        private readonly IStore<UserLogin> _userLoginStore;
        private readonly IStore<Log> _logStore;


        public GlobalController(IStore<Menu> menuStore,
            IStore<Role> roleStore,
            IStore<User> userStore,
            IStore<UserLogin> userLoginStore,
            IStore<Log> logStore)
        {
            _menuStore = menuStore;
            _roleStore = roleStore;
            _userStore = userStore;
            _userLoginStore = userLoginStore;
            _logStore = logStore;
        }


        public IActionResult Menus(int? size, int? page)
        {
            ViewBag.MenuStore = _menuStore;

            return View(_menuStore.GetPagingByRank(page, size));
        }


        public IActionResult Roles(int? size, int? page)
        {
            return View(_roleStore.GetPagingById(page, size));
        }


        public IActionResult UserLogins(int? size, int? page)
        {
            ViewBag.UserStore = _userStore;

            return View(_userLoginStore.GetDescendingPagingById(page, size));
        }


        public IActionResult Logs(int? size, int? page)
        {
            ViewBag.UserStore = _userStore;

            return View(_logStore.GetDescendingPagingById(page, size));
        }

    }
}
