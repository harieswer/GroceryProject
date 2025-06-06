using ApplicationCore.DbScript;
using ApplicationCore.Interfaces;
using Infrastracture.Dbscript;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                    _ = services.AddScoped<IDbScript, NPGDbScript>();
                    break;
            }
            _ = services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }


    }
}
