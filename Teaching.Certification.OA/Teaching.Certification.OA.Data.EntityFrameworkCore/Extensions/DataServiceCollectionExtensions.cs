#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// <see cref="IServiceCollection"/> 静态扩展。
    /// </summary>
    public static class DataServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 OA 数据服务集合。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddOAData(this IServiceCollection services)
        {
            services.NotNull(nameof(services));

            services.AddScoped(typeof(IStore<>), typeof(EntityStore<>));

            services.AddScoped<IAccessor>(sp => sp.GetRequiredService<DbContextAccessor>());

            services.AddSingleton<IPasswordHashService, PasswordHashService>();

            return services;
        }

    }
}
