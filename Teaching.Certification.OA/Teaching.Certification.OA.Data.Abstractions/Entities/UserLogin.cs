#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class UserLogin : AbstractIdentifier<int>
    {
        public virtual string? UserId { get; set; }

        public virtual string? UserIp { get; set; }

        public virtual string? Descr { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual UserLoginStatus Status { get; set; }
    }
}
