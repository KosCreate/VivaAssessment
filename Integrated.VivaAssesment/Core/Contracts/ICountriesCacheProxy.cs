using Application.Dtos.Responses;

namespace Application.Contracts {
    public interface ICountriesCacheProxy {
        Task<CountriesResponse?> Get(CancellationToken cancellationToken);
        Task Set(CountriesResponse obj, CancellationToken cancellationToken);
    }
}
