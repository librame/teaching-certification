using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teaching.Certification.OA.AspNetMvc.Controllers
{
    using Data;

    [Authorize]
    public class PersonnelController : Controller
    {
        private readonly IStore<Branch> _branchStore;
        private readonly IStore<Department> _departmentStore;
        private readonly IStore<User> _userStore;
        private readonly IStore<Role> _roleStore;


        public PersonnelController(IStore<Branch> branchStore,
            IStore<Department> departmentStore,
            IStore<User> userStore,
            IStore<Role> roleStore)
        {
            _branchStore = branchStore;
            _departmentStore = departmentStore;
            _userStore = userStore;
            _roleStore = roleStore;
        }


        public IActionResult Branches(int? page, int? size)
        {
            return View(_branchStore.GetPagingById(page, size));
        }

        [HttpPost]
        public IActionResult Branches([FromForm]string id,
            [FromForm]string name, [FromForm]string abbrname)
        {
            if (ModelState.IsValid)
            {
                if (id.IsEmpty())
                {
                    var branch = new Branch
                    {
                        Name = name,
                        AbbrName = abbrname
                    };

                    _branchStore.AddIfNotExists(branch, p => p.Name == name);
                }
                else
                {
                    var branch = _branchStore.GetById(int.Parse(id));

                    if (name.IsNotEmpty() && name != branch.Name)
                        branch.Name = name;

                    if (abbrname.IsNotEmpty() && abbrname != branch.AbbrName)
                        branch.AbbrName = abbrname;
                }

                _branchStore.Accessor.SaveChanges();
            }

            return RedirectToAction(nameof(Branches));
        }


        public IActionResult Departments(int? page, int? size)
        {
            ViewBag.BranchStore = _branchStore;
            ViewBag.UserStore = _userStore;

            return View(_departmentStore.GetPagingById(page, size));
        }


        public IActionResult Users(int? page, int? size)
        {
            ViewBag.RoleStore = _roleStore;

            return View(_userStore.GetPagingById<User, string>(page, size));
        }

    }
}
