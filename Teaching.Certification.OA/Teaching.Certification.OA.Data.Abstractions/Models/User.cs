#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class User
    {
        public virtual string Id { get; set; }

        public virtual int RoleId { get; set; }

        public virtual int StateId { get; set; }

        public virtual int DepartId { get; set; }

        public virtual int Gender { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }
    }
}
