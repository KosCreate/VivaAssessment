namespace Application.Domain {
    public sealed class CountryEntity : Entity {
        public string CommonName { get; set; } = string.Empty;

        public string? Capital { get; set; }

        public string Borders { get; set; } = string.Empty;
    }
}
