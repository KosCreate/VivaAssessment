using Application.Contracts;
using Application.Dtos.Responses;
using Infrastructure.Dtos;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.Services;

public sealed class CountriesHttpClient : ICountriesHttpClient {
    private readonly HttpClient _httpClient;
    private readonly CountriesHttpClientConfiguration _configuration;

    public CountriesHttpClient(
        HttpClient httpClient,
        IOptions<CountriesHttpClientConfiguration> configuration) {
        _httpClient = httpClient;
        _configuration = configuration.Value;

        _httpClient.BaseAddress = new Uri(_configuration.BaseUrl);
    }

    public async Task<CountriesResponse> GetAllCountriesAsync(CancellationToken cancellationToken) {
        var response = await _httpClient.GetAsync(_configuration.AllCountriesPath, cancellationToken);
        response.EnsureSuccessStatusCode();

        var countries = await response.Content.ReadFromJsonAsync<List<RestCountryResponseModel>>(
            cancellationToken: cancellationToken);

        if (countries is null || countries.Count == 0) {
            return new CountriesResponse();
        }

        return new CountriesResponse {
            Countries = countries.Select(MapToDto).ToList()
        };
    }

    private static CountryResponse MapToDto(RestCountryResponseModel model) {
        return new CountryResponse {
            CommonName = model.Name?.Common ?? string.Empty,
            Capital = model.Capital?.FirstOrDefault(),
            Borders = model.Borders?.ToList() ?? []
        };
    }
}