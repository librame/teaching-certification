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
    /// 抽象可树形化静态扩展。
    /// </summary>
    public static class TreeableExtensions
    {
        /// <summary>
        /// 转换为可树形集合。
        /// </summary>
        /// <typeparam name="T">指定实现 <see cref="IParentIdentifier{TId}"/> 的元素类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="items">给定的项集合。</param>
        /// <returns>返回树形化接口。</returns>
        public static ITreeable<T, TId> AsTreeing<T, TId>(this IEnumerable<T> items)
            where T : IParentIdentifier<TId>
            where TId : IEquatable<TId>
            => new TreeingCollection<T, TId>(items);

    }
}
