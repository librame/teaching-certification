#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Linq;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// <see cref="EntityStore{TEntity}"/> 静态扩展。
    /// </summary>
    public static class EntityStoreExtensions
    {
        private const string Unknown = "未知";


        /// <summary>
        /// 获取父级。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="parentId">给定的父标识。</param>
        /// <param name="valueFunc">给定的实体值工厂方法。</param>
        /// <returns>返回 <typeparamref name="TValue"/>。</returns>
        public static TValue? GetParent<TEntity, TId, TValue>(this IStore<TEntity> store, TId parentId,
            Func<TEntity, TValue> valueFunc)
            where TEntity : class
            where TId : IEquatable<TId>
        {
            store.NotNull(nameof(store));
            valueFunc.NotNull(nameof(valueFunc));

            var entity = store.GetById(parentId);

            if (entity is null)
                return default;

            return valueFunc.Invoke(entity);
        }


        #region Paging

        /// <summary>
        /// 转换为可升序分页集合（注：默认按标识字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> GetPagingById<TEntity>(this IStore<TEntity> store,
            int? size, int? page)
            where TEntity : class, IIdentifier<int>
            => store.GetPagingById<TEntity, int>(size, page);

        /// <summary>
        /// 转换为可倒序分页集合（注：默认按标识字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> GetDescendingPagingById<TEntity>(this IStore<TEntity> store,
            int? size, int? page)
            where TEntity : class, IIdentifier<int>
            => store.GetDescendingPagingById<TEntity, int>(size, page);

        /// <summary>
        /// 转换为可升序分页集合（注：默认按标识字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> GetPagingById<TEntity, TId>(this IStore<TEntity> store,
            int? size, int? page)
            where TEntity : class, IIdentifier<TId>
            where TId : IEquatable<TId>
        {
            store.NotNull(nameof(store));

            return store.Queryable.AsPagingByIndexOfId<TEntity, TId>(page, size);
        }

        /// <summary>
        /// 转换为可倒序分页集合（注：默认按标识字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TId">指定的标识类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> GetDescendingPagingById<TEntity, TId>(this IStore<TEntity> store,
            int? size, int? page)
            where TEntity : class, IIdentifier<TId>
            where TId : IEquatable<TId>
        {
            store.NotNull(nameof(store));

            return store.Queryable.AsDescendingPagingByIndexOfId<TEntity, TId>(page, size);
        }


        /// <summary>
        /// 转换为可升序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> GetPagingByRank<TEntity>(this IStore<TEntity> store,
            int? size, int? page)
            where TEntity : class, IRanking<float>
            => store.GetPagingByRank<TEntity, float>(size, page);

        /// <summary>
        /// 转换为可倒序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> GetDescendingPagingByRank<TEntity>(this IStore<TEntity> store,
            int? size, int? page)
            where TEntity : class, IRanking<float>
            => store.GetDescendingPagingByRank<TEntity, float>(size, page);

        /// <summary>
        /// 转换为可升序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TRank">指定的排序类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> GetPagingByRank<TEntity, TRank>(this IStore<TEntity> store,
            int? size, int? page)
            where TEntity : class, IRanking<TRank>
            where TRank : struct
        {
            store.NotNull(nameof(store));

            return store.Queryable.AsPagingByIndexOfRank<TEntity, TRank>(page, size);
        }

        /// <summary>
        /// 转换为可倒序分页集合（注：默认按排序字段进行排序）。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <typeparam name="TRank">指定的排序类型。</typeparam>
        /// <param name="store">给定的 <see cref="IStore{TEntity}"/>。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{TEntity}"/>。</returns>
        public static IPageable<TEntity> GetDescendingPagingByRank<TEntity, TRank>(this IStore<TEntity> store,
            int? size, int? page)
            where TEntity : class, IRanking<TRank>
            where TRank : struct
        {
            store.NotNull(nameof(store));

            return store.Queryable.AsDescendingPagingByIndexOfRank<TEntity, TRank>(page, size);
        }

        #endregion


        #region Department

        /// <summary>
        /// 获取指定部门标识的名称。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Role}"/>。</param>
        /// <param name="departmentId">给定的部门标识。</param>
        /// <returns>返回字符串。</returns>
        public static string GetDepartmentName(this IStore<Department> store, string? departmentId)
        {
            store.NotNull(nameof(store));

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (departmentId.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return Unknown;
            }

            return store.GetById(int.Parse(departmentId))?.Name ?? departmentId;
        }

        #endregion


        #region Menu

        /// <summary>
        /// 获取树形菜单集合。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Menu}"/>。</param>
        /// <returns>返回 <see cref="ITreeable{Menu, String}"/>。</returns>
        public static ITreeable<Menu, string> GetTreeMenus(this IStore<Menu> store)
        {
            store.NotNull(nameof(store));

            var menus = store.Queryable.ToList();
            return menus.AsTreeing<Menu, string>();
        }

        #endregion


        #region Role

        /// <summary>
        /// 获取指定角色标识的名称。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Role}"/>。</param>
        /// <param name="roleId">给定的角色标识。</param>
        /// <returns>返回字符串。</returns>
        public static string GetRoleName(this IStore<Role> store, string? roleId)
        {
            store.NotNull(nameof(store));

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (roleId.IsEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                return Unknown;
            }

            return store.GetById(int.Parse(roleId))?.Name ?? roleId;
        }

        #endregion


        #region Document

        /// <summary>
        /// 获取分页文档集合。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Document}"/>。</param>
        /// <param name="status">给定的文档状态。</param>
        /// <param name="key">给定的名称关键字。</param>
        /// <param name="size">给定的页大小（可选）。</param>
        /// <param name="page">给定的页索引（可选）。</param>
        /// <returns>返回 <see cref="IPageable{Document}"/>。</returns>
        public static IPageable<Document> GetPagingDocuments(this IStore<Document> store, DocumentStatus? status,
            string? key, int ? size, int? page)
        {
            var query = store.Queryable;

            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (key.IsNotEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                query = query.Where(p => p.Name.Contains(key));
#pragma warning restore CS8602 // 解引用可能出现空引用。
            }

            return query.AsDescendingPagingByIndexOfId(page, size);
        }

        #endregion

    }
}
