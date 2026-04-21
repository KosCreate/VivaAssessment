using Application.Dtos.Responses;

namespace Application.Contracts {
    /// <summary>
    /// Cache proxy interface for countries response
    /// </summary>
    public interface ICountriesCacheProxy {
        /// <summary>
        /// Gets all countries from cache if available, otherwise returns null
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CountriesResponse?> Get(CancellationToken cancellationToken);

        /// <summary>
        /// Sets the countries response in cache
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Set(CountriesResponse obj, CancellationToken cancellationToken);
    }
}
