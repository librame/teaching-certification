#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 文档状态。
    /// </summary>
    public enum DocumentStatus
    {
        /// <summary>
        /// 默认（正常）。
        /// </summary>
        Default = Normal,

        /// <summary>
        /// 已删除。
        /// </summary>
        Deleted = 1,

        /// <summary>
        /// 正常。
        /// </summary>
        Normal = 2
    }
}
