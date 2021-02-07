#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 可分页接口。
    /// </summary>
    /// <typeparam name="T">指定的类型。</typeparam>
    public interface IPageable<T> : IEnumerable<T>, IPagingInfo
    {
        /// <summary>
        /// 分页描述符。
        /// </summary>
        PagingDescriptor Descriptor { get; }
    }
}
