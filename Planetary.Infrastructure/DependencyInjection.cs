using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planetary.Application.Interfaces;
using Planetary.Infrastructure.Context;
using System.Reflection;

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

            // Register the IPlanetaryDbContext interface with the concrete PlanetaryContext
            services.AddScoped<IPlanetaryDbContext>(provider => 
                provider.GetRequiredService<PlanetaryContext>());

            // Add additional infrastructure services here
            // Example: services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
