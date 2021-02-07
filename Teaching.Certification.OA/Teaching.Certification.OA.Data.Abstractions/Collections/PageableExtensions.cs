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
    /// 抽象可分页静态扩展。
    /// </summary>
    public static class PageableExtensions
    {

        #region Select Paging

        /// <summary>
        /// 投影分页。
        /// </summary>
        /// <typeparam name="TSource">指定的来源类型。</typeparam>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="sources">给定的来源 <see cref="IPageable{TSource}"/>。</param>
        /// <param name="selector">给定的投影选择器。</param>
        /// <returns>返回 <see cref="IPageable{TResult}"/>。</returns>
        public static IPageable<TResult>? SelectPaging<TSource, TResult>(this IPageable<TSource>? sources,
            Func<TSource, TResult> selector)
        {
            if (sources.IsNull())
                return null;

#pragma warning disable CS8604 // 可能的 null 引用参数。
            return new PagingCollection<TResult>(sources.Select(selector).ToList(), sources.Descriptor);
#pragma warning restore CS8604 // 可能的 null 引用参数。
        }

        /// <summary>
        /// 投影分页。
        /// </summary>
        /// <typeparam name="TSource">指定的来源类型。</typeparam>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="sources">给定的来源 <see cref="IPageable{TSource}"/>。</param>
        /// <param name="selector">给定的投影选择器。</param>
        /// <returns>返回 <see cref="IPageable{TResult}"/>。</returns>
        public static IPageable<TResult>? SelectPaging<TSource, TResult>(this IPageable<TSource>? sources,
            Func<TSource, int, TResult> selector)
        {
            if (sources.IsNull())
                return null;

#pragma warning disable CS8604 // 可能的 null 引用参数。
            return new PagingCollection<TResult>(sources.Select(selector).ToList(), sources.Descriptor);
#pragma warning restore CS8604 // 可能的 null 引用参数。
        }

        #endregion


        #region ICollection Paging

        /// <summary>
        /// 转换为可分页集合（内存分页）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="rows">给定的 <see cref="IList{T}"/>。</param>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> AsPagingByIndex<T>(this ICollection<T>? rows, int index, int size)
            where T : class
            => rows.AsPaging(paging => paging.ComputeByIndex(index, size));

        /// <summary>
        /// 转换为可分页集合（内存分页）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="rows">给定的 <see cref="IList{T}"/>。</param>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的获取条数。</param>
        /// <returns>返回 <see cref="IPageable{T}"/>。</returns>
        public static IPageable<T> AsPagingBySkip<T>(this ICollection<T>? rows, int skip, int take)
            where T : class
            => rows.AsPaging(paging => paging.ComputeBySkip(skip, take));

        private static IPageable<T> AsPaging<T>(this ICollection<T>? rows, Action<PagingDescriptor> computeAction)
            where T : class
        {
            if (rows is null)
                return PagingCollection<T>.Empty;

            var descriptor = new PagingDescriptor(rows.Count);
            computeAction?.Invoke(descriptor);

            return new PagingCollection<T>(rows, descriptor);
        }

        #endregion


        #region Paging by Index

        /// <summary>
        /// 转换为可升序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="query">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsPagingByIndex<TEntity>(this IQueryable<TEntity> query,
            int index, int size)
            where TEntity : class, IRanking<float>
            => query.AsPagingByIndex<TEntity, float>(index, size);

        /// <summary>
        /// 转换为可升序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TRank">指定的排序类型。</typeparam>
        /// <param name="query">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsPagingByIndex<TEntity, TRank>(this IQueryable<TEntity> query,
            int index, int size)
            where TEntity : class, IRanking<TRank>
            where TRank : struct
            => query.AsPagingByIndex(q => q.OrderBy(k => k.Rank), index, size);


        /// <summary>
        /// 转换为可降序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="query">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsDescendingPagingByIndex<TEntity>(this IQueryable<TEntity> query,
            int index, int size)
            where TEntity : class, IRanking<float>
            => query.AsDescendingPagingByIndex<TEntity, float>(index, size);

        /// <summary>
        /// 转换为可降序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TRank">指定的排序类型。</typeparam>
        /// <param name="query">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsDescendingPagingByIndex<TEntity, TRank>(this IQueryable<TEntity> query,
            int index, int size)
            where TEntity : class, IRanking<TRank>
            where TRank : struct
            => query.AsPagingByIndex(q => q.OrderByDescending(k => k.Rank), index, size);


        /// <summary>
        /// 转换为可分页集合（注：需要对查询接口进行排序操作，否则 LINQ 会抛出未排序异常）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="query">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="orderedFactory">给定的排序方式。</param>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsPagingByIndex<TEntity>(this IQueryable<TEntity> query,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderedFactory,
            int index, int size)
            where TEntity : class
        {
            if (orderedFactory.IsNotNull())
                query = orderedFactory.Invoke(query);

            return query.AsPaging(paging => paging.ComputeByIndex(index, size));
        }

        #endregion


        #region Paging by Skip

        /// <summary>
        /// 转换为可升序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="queryable">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的获取条数。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsPagingBySkip<TEntity>(this IQueryable<TEntity> queryable,
            int skip, int take)
            where TEntity : class, IRanking<float>
            => queryable.AsPagingBySkip<TEntity, float>(skip, take);

        /// <summary>
        /// 转换为可升序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TRank">指定的排序类型。</typeparam>
        /// <param name="queryable">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的获取条数。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsPagingBySkip<TEntity, TRank>(this IQueryable<TEntity> queryable,
            int skip, int take)
            where TEntity : class, IRanking<TRank>
            where TRank : struct
            => queryable.AsPagingBySkip(q => q.OrderBy(k => k.Rank), skip, take);


        /// <summary>
        /// 转换为可降序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="queryable">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的获取条数。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsDescendingPagingBySkip<TEntity>(this IQueryable<TEntity> queryable,
            int skip, int take)
            where TEntity : class, IRanking<float>
            => queryable.AsDescendingPagingBySkip<TEntity, float>(skip, take);

        /// <summary>
        /// 转换为可降序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TRank">指定的排序类型。</typeparam>
        /// <param name="queryable">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的获取条数。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsDescendingPagingBySkip<TEntity, TRank>(this IQueryable<TEntity> queryable,
            int skip, int take)
            where TEntity : class, IRanking<TRank>
            where TRank : struct
            => queryable.AsPagingBySkip(q => q.OrderByDescending(k => k.Rank), skip, take);


        /// <summary>
        /// 转换为可分页集合（注：需要对查询接口进行排序操作，否则 LINQ 会抛出未排序异常）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="queryable">给定的 <see cref="IQueryable{TEntity}"/>。</param>
        /// <param name="orderedFactory">给定的排序方式。</param>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的获取条数。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> AsPagingBySkip<TEntity>(this IQueryable<TEntity> queryable,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderedFactory,
            int skip, int take)
            where TEntity : class
        {
            if (orderedFactory.IsNotNull())
                queryable = orderedFactory.Invoke(queryable);

            return queryable.AsPaging(paging => paging.ComputeBySkip(skip, take));
        }

        #endregion


        private static IPageable<TEntity> AsPaging<TEntity>(this IQueryable<TEntity> queryable,
            Action<PagingDescriptor> computeAction)
            where TEntity : class
        {
            queryable.NotNull(nameof(queryable));

            var descriptor = new PagingDescriptor(queryable.Count());
            computeAction?.Invoke(descriptor);
            
            var q = queryable;
            
            // 跳过条数
            if (descriptor.Skip > 0)
                q = q.Skip(descriptor.Skip);

            // 获取条数
            if (descriptor.Size > 0)
                q = q.Take(descriptor.Size);

            return new PagingCollection<TEntity>(q.ToList(), descriptor);
        }

    }
}
