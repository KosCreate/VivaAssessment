using Application.Dtos.Responses;

namespace Application.Contracts {
    /// <summary>
    /// Country service interface that defines methods for retrieving and saving country data from cache, database or external API
    /// </summary>
    public interface ICountriesService {
        /// <summary>
        /// Get all countries from cache, database or external API and return the response
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CountriesResponse> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Save all countries to the database if they don't exist
        /// </summary>
        /// <param name="countries"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveAllAsync(IReadOnlyCollection<CountryResponse> countries, CancellationToken cancellationToken);
    }
}