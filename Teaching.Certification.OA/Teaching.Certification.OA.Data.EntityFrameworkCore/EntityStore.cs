#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// 实体存储。
    /// </summary>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    public class EntityStore<TEntity> : IStore<TEntity>
        where TEntity : class
    {
        private readonly IAccessor _accessor;

        private DbContextAccessor? _contextAccessor;
        private DbSet<TEntity>? _table;


        /// <summary>
        /// 构造一个 <see cref="EntityStore{TEntity}"/>。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public EntityStore(IAccessor accessor)
        {
            _accessor = accessor.NotNull(nameof(accessor));
        }


        /// <summary>
        /// 访问器接口。
        /// </summary>
        public IAccessor Accessor
            => _accessor;

        /// <summary>
        /// 可查询接口。
        /// </summary>
        public IQueryable<TEntity> Queryable
            => Table;

        /// <summary>
        /// 通过禁用跟踪执行可查询接口。
        /// </summary>
        public IQueryable<TEntity> QueryableByNoTracking
            => Table.AsNoTracking();

        /// <summary>
        /// 实体表集合。
        /// </summary>
        public virtual DbSet<TEntity> Table
        {
            get
            {
                if (_table is null)
                    _table = EnsureDbContextAccessor().Set<TEntity>();

                return _table;
            }
        }


        /// <summary>
        /// 确保 <see cref="DbContextAccessor"/>。
        /// </summary>
        /// <returns>返回 <see cref="DbContextAccessor"/>。</returns>
        protected virtual DbContextAccessor EnsureDbContextAccessor()
        {
            if (_contextAccessor is null)
            {
                if (_accessor is DbContextAccessor contextAccessor)
                    _contextAccessor = contextAccessor;
                else
                    throw new NotSupportedException($"Unsupported accessor type '{_accessor.GetType()}'");
            }

            return _contextAccessor;
        }


        /// <summary>
        /// 通过标识获取实体。
        /// </summary>
        /// <param name="id">给定的标识。</param>
        /// <returns>返回 <typeparamref name="TEntity"/>。</returns>
        public virtual TEntity GetById(object id)
            => Table.Find(id);


        /// <summary>
        /// 如果不存在则添加实体。
        /// </summary>
        /// <param name="entity">给定要添加的实体。</param>
        /// <param name="predicate">给定用于判定是否存在的工厂方法。</param>
        public virtual void AddIfNotExists(TEntity entity, Func<TEntity, bool> predicate)
        {
            if (!Table.Any(predicate))
                Table.Add(entity);
        }

        /// <summary>
        /// 添加实体集合。
        /// </summary>
        /// <param name="entities">给定的实体数组集合。</param>
        public virtual void Add(params TEntity[] entities)
            => Table.AddRange(entities);

        /// <summary>
        /// 添加实体集合。
        /// </summary>
        /// <param name="entities">给定的 <see cref="IEnumerable{TEntity}"/>。</param>
        public virtual void Add(IEnumerable<TEntity> entities)
            => Table.AddRange(entities);


        /// <summary>
        /// 更新实体集合。
        /// </summary>
        /// <param name="entities">给定的实体数组集合。</param>
        public virtual void Update(params TEntity[] entities)
            => Table.UpdateRange(entities);

        /// <summary>
        /// 更新实体集合。
        /// </summary>
        /// <param name="entities">给定的 <see cref="IEnumerable{TEntity}"/>。</param>
        public virtual void Update(IEnumerable<TEntity> entities)
            => Table.UpdateRange(entities);


        /// <summary>
        /// 移除实体集合。
        /// </summary>
        /// <param name="entities">给定的实体数组集合。</param>
        public virtual void Remove(params TEntity[] entities)
            => Table.RemoveRange(entities);

        /// <summary>
        /// 移除实体集合。
        /// </summary>
        /// <param name="entities">给定的 <see cref="IEnumerable{TEntity}"/>。</param>
        public virtual void Remove(IEnumerable<TEntity> entities)
            => Table.RemoveRange(entities);

    }
}
