#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 可树形接口。
    /// </summary>
    /// <typeparam name="T">指定实现 <see cref="IParentIdentifier{TId}"/> 的元素类型。</typeparam>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public interface ITreeable<T, TId> : IEnumerable<TreeingNode<T, TId>>
        where T : IParentIdentifier<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 节点数。
        /// </summary>
        int Count { get; }
    }
}
