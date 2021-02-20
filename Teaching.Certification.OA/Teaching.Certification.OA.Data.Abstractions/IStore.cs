#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 存储接口。
    /// </summary>
    /// <typeparam name="T">指定的类型。</typeparam>
    public interface IStore<T>
        where T : class
    {
        /// <summary>
        /// 访问器接口。
        /// </summary>
        IAccessor Accessor { get; }

        /// <summary>
        /// 可查询接口。
        /// </summary>
        IQueryable<T> Queryable { get; }

        /// <summary>
        /// 通过禁用跟踪执行可查询接口。
        /// </summary>
        IQueryable<T> QueryableByNoTracking { get; }


        /// <summary>
        /// 通过标识获取类型实例。
        /// </summary>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回 <typeparamref name="T"/>。</returns>
        T GetById(object id);


        /// <summary>
        /// 如果不存在则添加类型实例。
        /// </summary>
        /// <param name="entity">给定要添加的类型实例。</param>
        /// <param name="predicate">给定用于判定是否存在的工厂方法。</param>
        void AddIfNotExists(T entity, Func<T, bool> predicate);

        /// <summary>
        /// 添加类型实例集合。
        /// </summary>
        /// <param name="items">给定的类型实例数组集合。</param>
        void Add(params T[] items);

        /// <summary>
        /// 添加类型实例集合。
        /// </summary>
        /// <param name="items">给定的 <see cref="IEnumerable{T}"/>。</param>
        void Add(IEnumerable<T> items);


        /// <summary>
        /// 更新类型实例集合。
        /// </summary>
        /// <param name="items">给定的类型实例数组集合。</param>
        void Update(params T[] items);

        /// <summary>
        /// 更新类型实例集合。
        /// </summary>
        /// <param name="items">给定的 <see cref="IEnumerable{T}"/>。</param>
        void Update(IEnumerable<T> items);


        /// <summary>
        /// 移除类型实例集合。
        /// </summary>
        /// <param name="items">给定的类型实例数组集合。</param>
        void Remove(params T[] items);

        /// <summary>
        /// 移除类型实例集合。
        /// </summary>
        /// <param name="items">给定的 <see cref="IEnumerable{T}"/>。</param>
        void Remove(IEnumerable<T> items);
    }
}
