using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudServices.Extensions
{
    public static class CloudServiceRegistration
    {
        public static IServiceCollection AddCloudServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            string? CloudPlatform = configuration.GetValue<string>("CloudPlatform");
            switch (CloudPlatform)
            {
                case "aws":
                     services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
                    break;
               
                    
            }
            return services;
        }
    }
}
