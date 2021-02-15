using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Teaching.Certification.OA.AspNetMvc.Controllers
{
    using Data;

    public class AccountController : Controller
    {
        private readonly IStore<User> _userStore;
        private readonly IStore<Role> _roleStore;
        private readonly IStore<Department> _departmentStore;
        private readonly IPasswordHashService _passwordHash;
        private readonly IUserProfileService _userProfile;


        public AccountController(IStore<User> userStore,
            IStore<Role> roleStore,
            IStore<Department> departmentStore,
            IPasswordHashService passwordHash,
            IUserProfileService userProfile)
        {
            _userStore = userStore;
            _roleStore = roleStore;
            _departmentStore = departmentStore;
            _passwordHash = passwordHash;
            _userProfile = userProfile;
        }


        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userStore.Queryable.SingleOrDefault(p => p.Name == model.UserName);
                if (user is null)
                {
                    ModelState.AddModelError(string.Empty, "用户不存在");
                    return View(model);
                }

                if (!_passwordHash.VerifyHash(user.PasswordHash, model.Password))
                {
                    ModelState.AddModelError(string.Empty, "密码不正确");
                    return View(model);
                }

                var role = _roleStore.GetById(user.RoleId);
                var department = _departmentStore.GetById(user.DepartmentId);

                var identity = _userProfile.PopulateIdentity(user, role, department);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }

            return View(model);
        }


        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userStore.Queryable.SingleOrDefault(p => p.Name == model.UserName);
                if (user.IsNotNull())
                {
                    ModelState.AddModelError(string.Empty, "用户已存在");
                    return View(model);
                }

                user = new User
                {
                    Name = model.UserName,
                    PasswordHash = _passwordHash.ComputeHash(model.Password),
                    Gender = model.IsMale ? UserGender.Male : UserGender.Female,
                };

                _userStore.Add(user);
                await _userStore.Accessor.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }


        [Authorize]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = _userProfile.GetCurrentUserName();
                var user = _userStore.Queryable.SingleOrDefault(p => p.Name == userName);
                if (user is null)
                {
                    ModelState.AddModelError(string.Empty, "用户不存在");
                    return View(model);
                }

                if (!_passwordHash.VerifyHash(user.PasswordHash, model.OldPassword))
                {
                    ModelState.AddModelError(string.Empty, "当前密码不正确");
                    return View(model);
                }

                user.PasswordHash = _passwordHash.ComputeHash(model.NewPassword);
                await _userStore.Accessor.SaveChangesAsync();

                await HttpContext.SignOutAsync();

                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Login();
        }

    }
}
