using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly IPasswordHashService _passwordHash;
        private readonly IUserProfileService _userProfile;


        public AccountController(IStore<User> userStore,
            IPasswordHashService passwordHash,
            IUserProfileService userProfile)
        {
            _userStore = userStore;
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
                var user = _userStore.Queryable.SingleOrDefault(p => p.UserName == model.UserName);
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

                var identity = _userProfile.PopulateIdentity(user);
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
                //var user = _userStore.Queryable.SingleOrDefault(p => p.UserName == model.UserName);
                //if (user is null)
                //{
                //    ModelState.AddModelError(string.Empty, "用户不存在");
                //    return View(model);
                //}

                //if (!_passwordHash.VerifyHash(user.PasswordHash, model.Password))
                //{
                //    ModelState.AddModelError(string.Empty, "密码不正确");
                //    return View(model);
                //}

                //var identity = new ClaimsIdentity();

                //identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
                //identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(identity));
            }

            return View(model);
        }


        public IActionResult ChangePassword()
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
