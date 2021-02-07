#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 分页信息接口。
    /// </summary>
    public interface IPagingInfo
    {
        /// <summary>
        /// 总条数。
        /// </summary>
        int Total { get; }

        /// <summary>
        /// 总页数。
        /// </summary>
        int Pages { get; }

        /// <summary>
        /// 跳过的条数。
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// 页大小或得到的条数。
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 页索引。
        /// </summary>
        int Index { get; }
    }
}
