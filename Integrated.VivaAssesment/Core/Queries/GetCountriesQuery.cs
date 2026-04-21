using Application.Contracts;
using Application.Dtos.Responses;
using MediatR;

namespace Application.Queries {
    public record GetCountriesQuery : IRequest<CountriesResponse> { }

    public sealed class GetCountriesQueryHandler(ICountriesService countriesService) : IRequestHandler<GetCountriesQuery, CountriesResponse> {
        private readonly ICountriesService _countriesService = countriesService;

        public async Task<CountriesResponse> Handle(GetCountriesQuery request, CancellationToken cancellationToken) {
            return await _countriesService.GetAllAsync(cancellationToken);
        }
    }
}
