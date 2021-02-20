#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// <see cref="IStore{T}"/> 与 <see cref="IPageable{T}"/> 静态扩展。
    /// </summary>
    public static class StorePageableExtensions
    {

        #region By IIdentifier

        /// <summary>
        /// 转换为可升序分页集合（注：类型需实现 <see cref="IIdentifier{Int32}"/>；默认按标识字段进行排序）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> GetPagingById<T>(this IStore<T> store,
            int? page, int? size)
            where T : class, IIdentifier<int>
            => store.GetPagingById<T, int>(size, page);

        /// <summary>
        /// 转换为可倒序分页集合（注：类型需实现 <see cref="IIdentifier{Int32}"/>；默认按标识字段进行排序）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> GetDescendingPagingById<T>(this IStore<T> store,
            int? page, int? size)
            where T : class, IIdentifier<int>
            => store.GetDescendingPagingById<T, int>(size, page);


        /// <summary>
        /// 转换为可升序分页集合（注：类型需实现 <see cref="IIdentifier{TId}"/>；默认按标识字段进行排序）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> GetPagingById<T, TId>(this IStore<T> store,
            int? page, int? size)
            where T : class, IIdentifier<TId>
            where TId : IEquatable<TId>
        {
            store.NotNull(nameof(store));

            // store.QueryableByNoTracking
            return store.Queryable.AsPagingByIndexOfId<T, TId>(page, size);
        }

        /// <summary>
        /// 转换为可倒序分页集合（注：类型需实现 <see cref="IIdentifier{TId}"/>；默认按标识字段进行排序）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> GetDescendingPagingById<T, TId>(this IStore<T> store,
            int? page, int? size)
            where T : class, IIdentifier<TId>
            where TId : IEquatable<TId>
        {
            store.NotNull(nameof(store));

            // store.QueryableByNoTracking
            return store.Queryable.AsDescendingPagingByIndexOfId<T, TId>(page, size);
        }

        #endregion


        #region By IRanking

        /// <summary>
        /// 转换为可升序分页集合（注：类型需实现 <see cref="IRanking{Single}"/>；默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> GetPagingByRank<T>(this IStore<T> store,
            int? page, int? size)
            where T : class, IRanking<float>
            => store.GetPagingByRank<T, float>(size, page);

        /// <summary>
        /// 转换为可倒序分页集合（注：类型需实现 <see cref="IRanking{Single}"/>；默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> GetDescendingPagingByRank<T>(this IStore<T> store,
            int? page, int? size)
            where T : class, IRanking<float>
            => store.GetDescendingPagingByRank<T, float>(size, page);


        /// <summary>
        /// 转换为可升序分页集合（注：类型需实现 <see cref="IRanking{TRank}"/>；默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TRank">指定的排序类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> GetPagingByRank<T, TRank>(this IStore<T> store,
            int? page, int? size)
            where T : class, IRanking<TRank>
            where TRank : struct
        {
            store.NotNull(nameof(store));

            // store.QueryableByNoTracking
            return store.Queryable.AsPagingByIndexOfRank<T, TRank>(page, size);
        }

        /// <summary>
        /// 转换为可倒序分页集合（注：类型需实现 <see cref="IRanking{TRank}"/>；默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TRank">指定的排序类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{T}"/>。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> GetDescendingPagingByRank<T, TRank>(this IStore<T> store,
            int? page, int? size)
            where T : class, IRanking<TRank>
            where TRank : struct
        {
            store.NotNull(nameof(store));

            // store.QueryableByNoTracking
            return store.Queryable.AsDescendingPagingByIndexOfRank<T, TRank>(page, size);
        }

        #endregion

    }
}
