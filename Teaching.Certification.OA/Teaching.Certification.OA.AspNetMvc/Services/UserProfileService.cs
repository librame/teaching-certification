using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Teaching.Certification.OA.AspNetMvc
{
    using Data;

    class UserProfileService : IUserProfileService
    {
        private readonly IHttpContextAccessor _accessor;


        public UserProfileService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }


        public string GetCurrentUserId()
        {
            var identity = _accessor.HttpContext.User.Identities.FirstOrDefault(p => p.IsAuthenticated);
            return identity?.FindFirst(ClaimTypes.Sid)?.Value;
        }

        public string GetCurrentUserRoleId()
        {
            var identity = _accessor.HttpContext.User.Identities.FirstOrDefault(p => p.IsAuthenticated);
            return identity?.FindFirst(ClaimTypes.Role)?.Value;
        }

        public string GetCurrentUserDepartmentId()
        {
            var identity = _accessor.HttpContext.User.Identities.FirstOrDefault(p => p.IsAuthenticated);
            return identity?.FindFirst(ClaimTypes.UserData)?.Value;
        }

        public string GetCurrentUserGender()
        {
            var identity = _accessor.HttpContext.User.Identities.FirstOrDefault(p => p.IsAuthenticated);
            return identity?.FindFirst(ClaimTypes.Gender)?.Value;
        }

        public string GetCurrentUserName()
        {
            var identity = _accessor.HttpContext.User.Identities.FirstOrDefault(p => p.IsAuthenticated);
            return identity?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public ClaimsIdentity PopulateIdentity(User user)
        {
            user.NotNull(nameof(user));

            var identity = new ClaimsIdentity();

            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.RoleId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.UserData, user.DepartmentId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Gender, user.Gender.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            return identity;
        }

    }
}
