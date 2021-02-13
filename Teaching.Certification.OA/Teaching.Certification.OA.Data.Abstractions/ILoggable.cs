#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 可用于日志记录接口。
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// 转换为日志。
        /// </summary>
        /// <param name="userId">给定的用户标识（可选）。</param>
        /// <returns>返回 <see cref="LogDescriptor"/>。</returns>
        LogDescriptor ToLog(string? userId = null);
    }
}
