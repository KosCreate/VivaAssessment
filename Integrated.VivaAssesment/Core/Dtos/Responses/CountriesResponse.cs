namespace Application.Dtos.Responses {
    public sealed class CountriesResponse {
        public IReadOnlyCollection<CountryResponse> Countries { get; set; } = [];
    }
}
