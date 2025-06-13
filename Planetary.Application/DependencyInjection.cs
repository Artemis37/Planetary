using Microsoft.Extensions.DependencyInjection;

namespace Planetary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services here
            //services.AddScoped<IPlanetaryDbContext, PlanetaryDbContext>();
            
            return services;
        }
    }
}
