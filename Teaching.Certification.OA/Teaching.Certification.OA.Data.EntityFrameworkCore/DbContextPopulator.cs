#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// <see cref="DbContext"/> 填充器。
    /// </summary>
    public static class DbContextPopulator
    {
        ///// <summary>
        ///// 是否已填充。
        ///// </summary>
        ///// <returns>返回是否已填充的布尔值。</returns>
        //public static async Task<bool> IsPopulatedAsync(DbContextAccessor accessor, IServiceProvider services,
        //    CancellationToken cancellationToken = default)
        //{
        //    var isCreated = await accessor.Database.EnsureCreatedAsync(cancellationToken);
        //}


        /// <summary>
        /// 开始填充。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="Task"/>。</returns>
        public static async Task OnPopulateAsync(DbContextAccessor accessor, IServiceProvider services,
            CancellationToken cancellationToken = default)
        {
            var isCreated = await accessor.Database.EnsureCreatedAsync(cancellationToken);


        }

    }
}
