#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System.Security.Claims;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 用户个人资料服务。
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// 获取当前用户标识。
        /// </summary>
        /// <returns>返回字符串。</returns>
        string? GetCurrentUserId();

        /// <summary>
        /// 获取当前用户角色。
        /// </summary>
        /// <returns>返回字符串。</returns>
        string? GetCurrentUserRole();

        /// <summary>
        /// 获取当前用户部门。
        /// </summary>
        /// <returns>返回字符串。</returns>
        string? GetCurrentUserDepartment();

        /// <summary>
        /// 获取当前用户性别。
        /// </summary>
        /// <returns>返回字符串。</returns>
        string? GetCurrentUserGender();

        /// <summary>
        /// 获取当前用户名称。
        /// </summary>
        /// <returns>返回字符串。</returns>
        string? GetCurrentUserName();

        /// <summary>
        /// 填充声明身份。
        /// </summary>
        /// <param name="user">给定的 <see cref="User"/>。</param>
        /// <param name="role">给定的 <see cref="Role"/>。</param>
        /// <param name="department">给定的 <see cref="Department"/>。</param>
        /// <param name="authenticationType">给定的认证类型。</param>
        /// <returns>返回 <see cref="ClaimsIdentity"/>。</returns>
        ClaimsIdentity PopulateIdentity(User user, Role role, Department department, string? authenticationType);
    }
}
