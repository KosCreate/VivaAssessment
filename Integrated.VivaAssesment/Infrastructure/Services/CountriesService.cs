using Application.Contracts;
using Application.Domain;
using Application.Dtos.Responses;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public sealed class CountriesService(
    AppDbContext dbContext,
    ICountriesCacheProxy countriesCacheProxy,
    ICountriesHttpClient countriesHttpClient) : ICountriesService {
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ICountriesCacheProxy _countriesCacheProxy = countriesCacheProxy;
    private readonly ICountriesHttpClient _countriesHttpClient = countriesHttpClient;

    /// <inheritdoc/>
    public async Task<CountriesResponse> GetAllAsync(CancellationToken cancellationToken) {
        var cachedCountries = await _countriesCacheProxy.Get(cancellationToken);
        if (cachedCountries is not null && cachedCountries.Countries.Count > 0)
            return cachedCountries;

        var storedCountries = await GetFromDatabaseAsync(cancellationToken);

        if (storedCountries.Countries.Count > 0) {
            await _countriesCacheProxy.Set(storedCountries, cancellationToken);
            return storedCountries;
        }

        var fetchedCountries = await _countriesHttpClient.GetAllCountriesAsync(cancellationToken);

        if (fetchedCountries.Countries.Count > 0) {
            await SaveAllAsync(fetchedCountries.Countries, cancellationToken);
            await _countriesCacheProxy.Set(fetchedCountries, cancellationToken);
        }

        return fetchedCountries;
    }

    /// <inheritdoc/>
    public async Task SaveAllAsync(
        IReadOnlyCollection<CountryResponse> countries,
        CancellationToken cancellationToken) {

        if (countries.Count == 0) 
            return;

        var existingNames = await _dbContext.Countries
            .Select(x => x.CommonName)
            .ToListAsync(cancellationToken);

        var entitiesToInsert = countries
            .Where(x => !existingNames.Contains(x.CommonName, StringComparer.OrdinalIgnoreCase))
            .Select(MapToEntity)
            .ToList();

        if (entitiesToInsert.Count == 0)
            return;

        await _dbContext.Countries.AddRangeAsync(entitiesToInsert, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Helper method to get all countries from the database and map them to the response model
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<CountriesResponse> GetFromDatabaseAsync(CancellationToken cancellationToken) {
        var countries = await _dbContext.Countries
            .AsNoTracking()
            .OrderBy(x => x.CommonName)
            .ToListAsync(cancellationToken);

        return new CountriesResponse {
            Countries = countries.Select(MapToResponse).ToList()
        };
    }

    /// <summary>
    /// Maps a CountryEntity to a CountryResponse, splitting the Borders string into a list of strings - that is because the external API returns 
    /// a list of borders but we store them as a comma-separated string in the database for simplicity. This method handles the conversion between the two formats.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private static CountryResponse MapToResponse(CountryEntity entity) {
        var borders = string.IsNullOrWhiteSpace(entity.Borders)
            ? []
            : entity.Borders
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();

        return new CountryResponse {
            CommonName = entity.CommonName,
            Capital = entity.Capital,
            Borders = borders
        };
    }

    /// <summary>
    /// Maps a CountryResponse to a CountryEntity, joining the Borders list into a comma-separated string (for the above reason)
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private static CountryEntity MapToEntity(CountryResponse response) =>
        new CountryEntity() {
            CommonName = response.CommonName,
            Capital = response.Capital,
            Borders = response.Borders is null || response.Borders.Count == 0
                ? string.Empty
                : string.Join(',', response.Borders)
        };
}