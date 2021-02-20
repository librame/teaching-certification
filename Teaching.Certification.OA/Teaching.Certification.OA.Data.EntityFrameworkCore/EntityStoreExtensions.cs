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


        #region Document

        /// <summary>
        /// 获取分页文档集合。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Document}"/>。</param>
        /// <param name="status">给定的文档状态。</param>
        /// <param name="key">给定的名称关键字。</param>
        /// <param name="page">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{Document}"/>。</returns>
        public static IPageable<Document> GetPagingDocuments(this IStore<Document> store, DocumentStatus? status,
            string? key, int? page, int? size)
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


        #region Schedule

        /// <summary>
        /// 获取分页日程集合。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Schedule}"/>。</param>
        /// <param name="beginTime">给定的日程开始时间。</param>
        /// <param name="departmentId">给定的部门标识。</param>
        /// <param name="creatorId">给定的用户标识。</param>
        /// <param name="page">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{Schedule}"/>。</returns>
        public static IPageable<Schedule> GetPagingSchedules(this IStore<Schedule> store, DateTime? beginTime,
            int? departmentId, string? creatorId, int? page, int? size)
        {
            var query = store.Queryable;

            if (beginTime.HasValue)
                query = query.Where(p => p.BeginTime.Date == beginTime.Value.Date);

            if (departmentId.HasValue)
                query = query.Where(p => p.DepartmentId == departmentId.Value);

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (creatorId.IsNotEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                query = query.Where(p => p.CreatorId == creatorId);
            }

            return query.AsPagingByIndex(ordered => ordered.OrderBy(ks => ks.BeginTime), page, size);
        }

        #endregion


        #region Note

        /// <summary>
        /// 获取分页便签集合。
        /// </summary>
        /// <param name="store">给定的 <see cref="IStore{Note}"/>。</param>
        /// <param name="creatorId">给定的用户标识。</param>
        /// <param name="page">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <returns>返回 <see cref="IPageable{Note}"/>。</returns>
        public static IPageable<Note> GetPagingNotes(this IStore<Note> store,
            string? creatorId, int? page, int? size)
        {
            var query = store.Queryable;

#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (creatorId.IsNotEmpty())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                query = query.Where(p => p.CreatorId == creatorId);
            }

            return query.AsPagingByIndex(ordered => ordered.OrderBy(ks => ks.CreatedTime), page, size);
        }

        #endregion

    }
}
