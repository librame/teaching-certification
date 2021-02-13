#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class User : AbstractIdentifier<string>, ILoggable
    {
        public virtual int RoleId { get; set; }

        public virtual int StateId { get; set; }
            = 1;

        public virtual int DepartmentId { get; set; }

        public virtual UserGender Gender { get; set; }
            = UserGender.Male;

        public virtual string? UserName { get; set; }

        public virtual string? PasswordHash { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id,
                Name = UserName,
                Descr = $"{nameof(RoleId)}={RoleId},{nameof(StateId)}={StateId},{nameof(DepartmentId)}={DepartmentId},{nameof(Gender)}={Gender},{nameof(PasswordHash)}={PasswordHash}"
            };
        }

    }
}
