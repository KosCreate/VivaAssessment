using System.Text.Json.Serialization;
namespace Infrastructure.Dtos {
    public sealed class RestCountryResponseModel {
        [JsonPropertyName("name")]
        public RestCountriesNameModel Name { get; set; } = new();

        [JsonPropertyName("capital")]
        public List<string>? Capital { get; set; }

        [JsonPropertyName("borders")]
        public List<string>? Borders { get; set; }
    }

    public sealed class RestCountriesNameModel {
        [JsonPropertyName("common")]
        public string Common { get; set; } = string.Empty;
    }
}
