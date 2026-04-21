namespace Infrastructure.Options {
    public sealed class CountriesCacheConfiguration {
        public int AbsoluteExpirationMinutes { get; set; }
        public string CacheKey { get; set; } = string.Empty;
    }
}
