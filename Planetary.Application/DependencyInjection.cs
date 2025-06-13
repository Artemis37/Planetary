using Microsoft.Extensions.DependencyInjection;
using Planetary.Application.Common.Interfaces;

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
