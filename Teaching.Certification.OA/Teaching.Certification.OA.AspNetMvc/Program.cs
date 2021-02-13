using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Teaching.Certification.OA.AspNetMvc
{
    using Data;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .InitializeAccessor<DbContextAccessor>((accessor, services) =>
                {
                    DbContextPopulator.OnPopulateAsync(accessor, services).Wait();
                })
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
