using Application.Dtos.Responses;

namespace Application.Contracts {
    public interface ICountriesService {
        Task<CountriesResponse> GetAllAsync(CancellationToken cancellationToken);
        Task SaveAllAsync(IReadOnlyCollection<CountryResponse> countries, CancellationToken cancellationToken);
    }
}