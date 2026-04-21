using Application.Contracts;
using Infrastructure.DbContexts;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection {
    public static class InfraServiceCollectionExtensions {
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
