#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Teaching.Certification.OA.Data
{
    public static class DbSetExtensions
    {
        /// <summary>
        /// 数据集是否已填充。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="dbSet">给定的 <see cref="DbSet{TEntity}"/>。</param>
        /// <returns>返回是否已填充的布尔值。</returns>
        public static bool IsPopulated<TEntity>(this DbSet<TEntity> dbSet)
            where TEntity : class
        {
            if (dbSet.Any())
                return true;

            return dbSet.Local.Any();
        }

        /// <summary>
        /// 准备查询。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="dbSet">给定的 <see cref="DbSet{TEntity}"/>。</param>
        /// <returns>返回 <see cref="IQueryable{TEntity}"/>。</returns>
        public static IQueryable<TEntity> ReadyQuery<TEntity>(this DbSet<TEntity> dbSet)
            where TEntity : class
        {
            if (dbSet.Any())
                return dbSet;

            return dbSet.Local.AsQueryable();
        }

    }
}
