#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System.Linq;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// <see cref="EntityStore{TEntity}"/> 静态扩展。
    /// </summary>
    public static class EntityStoreExtensions
    {
        private const string Unknown = "未知";


        #region Department

        /// <summary>
        /// 获取指定部门标识的名称。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Role}"/>。</param>
        /// <param name="departmentId">给定的部门标识。</param>
        /// <returns>返回字符串。</returns>
        public static string GetDepartmentName(this IStore<Department> store, string? departmentId)
        {
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (departmentId.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return Unknown;
            }

            return store.GetById(int.Parse(departmentId))?.Name ?? departmentId;
        }

        #endregion


        #region Menu

        /// <summary>
        /// 获取树形菜单集合。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Menu}"/>。</param>
        /// <returns>返回 <see cref="ITreeable{Menu, String}"/>。</returns>
        public static ITreeable<Menu, string> GetTreeMenus(this IStore<Menu> store)
        {
            var menus = store.Queryable.ToList();
            return menus.AsTreeing<Menu, string>();
        }

        #endregion


        #region Role

        /// <summary>
        /// 获取指定角色标识的名称。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Role}"/>。</param>
        /// <param name="roleId">给定的角色标识。</param>
        /// <returns>返回字符串。</returns>
        public static string GetRoleName(this IStore<Role> store, string? roleId)
        {
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (roleId.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return Unknown;
            }

            return store.GetById(int.Parse(roleId))?.Name ?? roleId;
        }

        #endregion

    }
}
