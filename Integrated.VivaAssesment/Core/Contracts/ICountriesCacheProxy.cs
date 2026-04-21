using Application.Dtos.Responses;

namespace Application.Contracts {
    public interface ICountriesCacheProxy {
        Task<CountriesResponse?> GetAsync(CancellationToken cancellationToken);
        Task SetAsync(CountriesResponse obj, CancellationToken cancellationToken);
    }
}
