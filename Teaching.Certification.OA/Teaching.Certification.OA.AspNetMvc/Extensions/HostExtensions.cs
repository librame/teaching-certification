using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Teaching.Certification.OA.AspNetMvc
{
    using Data;

    public static class HostExtensions
    {
        /// <summary>
        /// 初始化访问器。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <param name="host">给定的 <see cref="IHost"/>。</param>
        /// <param name="initalAction">给定的初始化动作。</param>
        /// <returns>返回 <see cref="IHost"/>。</returns>
        public static IHost InitializeAccessor<TAccessor>(this IHost host,
            Action<TAccessor, IServiceProvider> initalAction)
            where TAccessor : DbContextAccessor
        {
            host.NotNull(nameof(host));

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TAccessor>>();
                var accessor = services.GetService<TAccessor>();

                try
                {
                    initalAction?.Invoke(accessor, services);

                    logger.LogInformation($"初始化数据库访问器”{typeof(TAccessor).Name}“成功");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"初始化数据库访问器”{typeof(TAccessor).Name}“失败");
                }
            }

            return host;
        }

    }
}
