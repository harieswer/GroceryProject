using ApplicationCore.Cache;
using ApplicationCore.DbScript;
using ApplicationCore.Interfaces;
using Infrastracture.Cache;
using Infrastracture.Dbscript;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
namespace Infrastracture.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInsfrastractionRegistrationServices(this IServiceCollection services, IConfiguration configurationManager)
        {
            string dbServer = configurationManager.GetSection("dbServer").Value;

            switch (dbServer)
            {
                case "postgresql":
                    services.AddScoped<IDbScript, PostgresqlDbScript>();
                    break;
            }
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.RegisterCacheService(configurationManager);
            return services;

        }

        public static IServiceCollection RegisterCacheService(this IServiceCollection services, IConfiguration configurationManager)
        {
            string? cache = configurationManager.GetSection("CacheService").Value;

            switch (cache)
            {
                   
                case "Distributed":

                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = "localhost"; // Replace with your Redis connection string
                        options.InstanceName = "MyApp:";
                    });
                    services.AddScoped<ICacheService, DistributedCacheService>();
                    break;
                case "Redis":
                    services.AddSingleton<IConnectionMultiplexer>(sp =>
                    ConnectionMultiplexer.Connect("localhost"));
                    services.AddScoped<ICacheService, RedisCacheService>();
                    break;

            default:
                    services.AddScoped<ICacheService, MemoryCacheService>();
                    services.AddMemoryCache();
                    break;
            }
            return services;
        }

    }
}
