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

        public string GetCurrentUserRole()
        {
            var identity = _accessor.HttpContext.User.Identities.FirstOrDefault(p => p.IsAuthenticated);
            return identity?.FindFirst(ClaimTypes.Role)?.Value;
        }

        public string GetCurrentUserDepartment()
        {
            var identity = _accessor.HttpContext.User.Identities.FirstOrDefault(p => p.IsAuthenticated);
            return identity?.FindFirst(ClaimTypes.GroupSid)?.Value;
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

        public ClaimsIdentity PopulateIdentity(User user, Role role, Department department, string authenticationType)
        {
            user.NotNull(nameof(user));

            var identity = new ClaimsIdentity(authenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Role, role?.Name ?? user.RoleId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.GroupSid, department?.Name ?? user.DepartmentId.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Gender, user.Gender.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));

            return identity;
        }

    }
}
