using Application.Contracts;
using Application.Dtos.Responses;
using Infrastructure.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public sealed class CountriesCacheProxy : ICountriesCacheProxy {
    private readonly IMemoryCache _memoryCache;
    private readonly CountriesCacheConfiguration _cacheConfiguration;

    public CountriesCacheProxy(
        IMemoryCache memoryCache,
        IOptions<CountriesCacheConfiguration> cacheConfiguration) {
        _memoryCache = memoryCache;
        _cacheConfiguration = cacheConfiguration.Value;
    }

    public Task<CountriesResponse?> GetAsync(CancellationToken cancellationToken) {
        _memoryCache.TryGetValue(_cacheConfiguration.CacheKey, out CountriesResponse? countriesResponse);

        return Task.FromResult(countriesResponse);
    }

    public Task SetAsync(CountriesResponse obj, CancellationToken cancellationToken) {
        var cacheEntryOptions = new MemoryCacheEntryOptions {
            AbsoluteExpirationRelativeToNow =
                TimeSpan.FromMinutes(_cacheConfiguration.AbsoluteExpirationMinutes)
        };

        _memoryCache.Set(_cacheConfiguration.CacheKey, obj, cacheEntryOptions);

        return Task.CompletedTask;
    }
}