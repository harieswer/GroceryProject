using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
