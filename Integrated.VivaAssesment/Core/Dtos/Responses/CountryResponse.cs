namespace Application.Dtos.Responses {
    public sealed class CountryResponse
    {
        public string CommonName { get; set; } = string.Empty;
        public string? Capital { get; set; }
        public IReadOnlyCollection<string> Borders { get; set; } = [];
    }
}
