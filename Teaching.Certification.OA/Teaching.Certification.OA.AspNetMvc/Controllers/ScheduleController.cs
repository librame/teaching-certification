using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teaching.Certification.OA.AspNetMvc.Controllers
{
    using Data;

    [Authorize(Roles = DbContextPopulator.RoleAdministrator)]
    public class ScheduleController : Controller
    {
        public IActionResult My()
        {
            return View();
        }

        public IActionResult Departments()
        {
            return View();
        }

        public IActionResult Notes()
        {
            return View();
        }

    }
}
