using Application.Contracts;
using Infrastructure.DbContexts;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection {
    /// <summary>
    /// Contains extension methods for registering infrastructure services in the DI container.
    /// </summary>
    public static class InfraServiceCollectionExtensions {

        /// <summary>
        /// Add infrastructure services to the DI container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<CountriesHttpClientConfiguration>(configuration.GetSection(nameof(CountriesHttpClientConfiguration)));
            services.Configure<CountriesCacheConfiguration>(configuration.GetSection(nameof(CountriesCacheConfiguration)));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddMemoryCache();

            services.AddHttpClient<ICountriesHttpClient, CountriesHttpClient>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<ICountriesCacheProxy, CountriesCacheProxy>();

            return services;
        }
    }
}
