using Application.Dtos.Responses;

namespace Application.Contracts {
    public interface ICountriesHttpClient {
        Task<CountriesResponse> GetAllCountriesAsync(CancellationToken cancellationToken = default);
    }
}
