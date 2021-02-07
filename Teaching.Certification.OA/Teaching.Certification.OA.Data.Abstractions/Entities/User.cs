#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class User : AbstractIdentifier<string>
    {
        public virtual int RoleId { get; set; }

        public virtual int StateId { get; set; }

        public virtual int DepartId { get; set; }

        public virtual int Gender { get; set; }

        public virtual string? UserName { get; set; }

        public virtual string? PasswordHash { get; set; }
    }
}
