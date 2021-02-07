#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class RoleMenu : AbstractIdentifier<int>
    {
        public virtual int RoleId { get; set; }

        public virtual int MenuId { get; set; }
    }
}
