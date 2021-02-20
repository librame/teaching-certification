#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    public class StoreHelper
    {
        /// <summary>
        /// 生成角色标识。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public static string GenerateRoleId()
            => Guid.NewGuid().ToString();

        /// <summary>
        /// 生成用户标识。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public static string GenerateUserId()
            => Guid.NewGuid().ToString();

    }
}
