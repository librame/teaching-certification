#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 密码服务接口。
    /// </summary>
    public interface IPasswordHashService
    {
        /// <summary>
        /// 计算密码哈希。
        /// </summary>
        /// <param name="password">给定的密码。</param>
        /// <returns>返回字符串。</returns>
        string ComputeHash(string password);

        /// <summary>
        /// 验证密码哈希。
        /// </summary>
        /// <param name="passwordHash">给定的密码哈希。</param>
        /// <param name="password">给定的密码。</param>
        /// <returns>返回是否相同的布尔值。</returns>
        bool VerifyHash(string passwordHash, string password);
    }
}
