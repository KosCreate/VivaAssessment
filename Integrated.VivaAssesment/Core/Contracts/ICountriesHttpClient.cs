using Application.Dtos.Responses;

namespace Application.Contracts {
    /// <summary>
    /// HttpClient for the external API that provides country data
    /// </summary>
    public interface ICountriesHttpClient {
        /// <summary>
        /// Gets all countries from the external API
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CountriesResponse> GetAllCountriesAsync(CancellationToken cancellationToken = default);
    }
}
