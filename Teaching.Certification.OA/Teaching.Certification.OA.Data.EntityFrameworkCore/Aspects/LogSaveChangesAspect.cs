#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Teaching.Certification.OA.Data
{
    class LogSaveChangesAspect : ISaveChangesAspect
    {
        /// <summary>
        /// 记录实体变化的状态集合（默认对实体的添加、修改、删除状态进行记录）。
        /// </summary>
        private IReadOnlyList<EntityState> LogEntityStates { get; }
            = new List<EntityState>
            {
                EntityState.Added,
                EntityState.Modified,
                EntityState.Deleted
            };


        public void PreInject(IAccessor accessor)
        {
            accessor.NotNull(nameof(accessor));

            var contextAccessor = accessor as DbContextAccessor;
            var currentUserId = contextAccessor.GetService<IUserProfileService>()?.GetCurrentUserId();

            var logs = GetLogs();

#pragma warning disable CS8602 // 解引用可能出现空引用。
            contextAccessor.Logs.AddRange(logs);
#pragma warning restore CS8602 // 解引用可能出现空引用。

            IEnumerable<Log> GetLogs()
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                var query = contextAccessor.ChangeTracker.Entries().Where(p
                    => p.Entity.IsNotNull() && p.Metadata.ClrType.IsImplementedInterfaceType<ILoggable>());
#pragma warning restore CS8602 // 解引用可能出现空引用。

                return query
                    .Where(p => LogEntityStates.Contains(p.State))
                    .Select(p => Log.CreateFromDescriptor(((ILoggable)p.Entity).ToLog(currentUserId)));
            }
        }

        public void PostInject(IAccessor accessor)
        {
        }

    }
}
