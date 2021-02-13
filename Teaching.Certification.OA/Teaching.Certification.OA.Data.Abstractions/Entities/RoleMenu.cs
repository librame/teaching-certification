#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    public class RoleMenu : AbstractIdentifier<int>, ILoggable
    {
        public virtual int RoleId { get; set; }

        public virtual string? MenuId { get; set; }


        public virtual LogDescriptor ToLog(string? userId = null)
        {
            return new LogDescriptor
            {
                UserId = userId,
                AssocId = Id.ToString(),
                Name = "关联角色菜单",
                Descr = $"{nameof(RoleId)}={RoleId},{nameof(MenuId)}={MenuId}"
            };
        }

    }
}
