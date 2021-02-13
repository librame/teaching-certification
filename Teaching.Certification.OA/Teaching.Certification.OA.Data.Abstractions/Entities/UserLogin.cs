#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class UserLogin : AbstractIdentifier<int>, ILoggable
    {
        public virtual string? UserId { get; set; }

        public virtual string? UserIp { get; set; }

        public virtual string? Descr { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual UserLoginStatus Status { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = "用户登录",
                Descr = $"{nameof(UserId)}={UserId},{nameof(UserIp)}={UserIp},{nameof(Descr)}={Descr},{nameof(CreatedTime)}={CreatedTime},{nameof(Status)}={Status}"
            };
        }

    }
}
