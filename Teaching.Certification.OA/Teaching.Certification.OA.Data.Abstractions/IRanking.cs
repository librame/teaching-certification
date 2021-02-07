#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 排名接口。
    /// </summary>
    /// <typeparam name="TRank">指定的排序类型（兼容整数、单双精度的排序字段）。</typeparam>
    public interface IRanking<TRank>
        where TRank : struct
    {
        /// <summary>
        /// 排名。
        /// </summary>
        TRank Rank { get; set; }
    }
}
