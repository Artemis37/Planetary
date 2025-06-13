using Microsoft.Extensions.Configuration;
using Planetary.Application;
using Planetary.Infrastructure;

namespace Planetary.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApplication()
                .AddInfrastructure(configuration);

            return services;
        }
    }
}
