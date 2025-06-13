using Planetary.Application;
using Planetary.Application.Interfaces;
using Planetary.Infrastructure;
using Planetary.Infrastructure.Repositories;

namespace Planetary.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApplication()
                .AddInfrastructure(configuration);

            services.AddScoped<ICriteriaRepository, CriteriaRepository>();

            return services;
        }
    }
}
