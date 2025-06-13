using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planetary.Application.Interfaces;
using Planetary.Infrastructure.Context;
using Planetary.Infrastructure.Repositories;

namespace Planetary.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration = null)
        {
            string connectionString = configuration?.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=PlanetaryDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            services.AddDbContext<PlanetaryContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(PlanetaryContext).Assembly.FullName);

                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);

                    sqlOptions.CommandTimeout(60);

                    if (configuration?.GetValue<bool>("Database:EnableSensitiveDataLogging") == true)
                    {
                        options.EnableSensitiveDataLogging();
                    }
                }));

            // Register repositories
            services.AddScoped<ICriteriaRepository, CriteriaRepository>();
            services.AddScoped<IPlanetRepository, PlanetRepository>();

            return services;
        }
    }
}
