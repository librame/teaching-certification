using Microsoft.Extensions.Hosting;
using System;

namespace Teaching.Certification.OA.AspNetMvc
{
    using Data;

    public static class DbContextAccessorHostExtensions
    {
        public static void InitializeDbContextSeek(this IHost host,
            Action<DbContextAccessor, IServiceProvider> seedAction)
        {

        }

    }
}
