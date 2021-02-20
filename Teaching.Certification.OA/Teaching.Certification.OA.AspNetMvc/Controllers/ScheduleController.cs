using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teaching.Certification.OA.AspNetMvc.Controllers
{
    using Data;

    [Authorize(Roles = DbContextPopulator.RoleNameAdministrator)]
    public class ScheduleController : Controller
    {
        private readonly IStore<Schedule> _scheduleStore;
        private readonly IStore<Department> _departmentStore;
        private readonly IStore<Note> _noteStore;
        private readonly IUserProfileService _userProfile;


        public ScheduleController(IStore<Schedule> scheduleStore,
            IStore<Department> departmentStore,
            IStore<Note> noteStore,
            IUserProfileService userProfile)
        {
            _scheduleStore = scheduleStore;
            _departmentStore = departmentStore;
            _noteStore = noteStore;
            _userProfile = userProfile;
        }


        public IActionResult My(int? page, int? size)
        {
            var currentUserId = _userProfile.GetCurrentUserId();

            return View(_scheduleStore.GetPagingSchedules(beginTime: null,
                departmentId: null, creatorId: currentUserId, page, size));
        }


        public IActionResult Departments(int? page, int? size)
        {
            var currentUserDepartment = _departmentStore.GetByName(_userProfile.GetCurrentUserDepartment());

            return View(_scheduleStore.GetPagingSchedules(beginTime: null,
                departmentId: currentUserDepartment?.Id, creatorId: null, page, size));
        }


        public IActionResult Notes(int? page, int? size)
        {
            var currentUserId = _userProfile.GetCurrentUserId();

            return View(_noteStore.GetPagingNotes(currentUserId, page, size));
        }

    }
}
