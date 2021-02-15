using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teaching.Certification.OA.AspNetMvc.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PersonnelController : Controller
    {
        public IActionResult Branches()
        {
            return View();
        }

        public IActionResult Departments()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

    }
}
