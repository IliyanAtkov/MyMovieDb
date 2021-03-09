using System.Text.Json.Serialization;

namespace MyMovieDb.Services.TheMovieDb.Models.Countries
{
    public class CountryResult
    {
        [JsonPropertyName("iso_3166_1")]
        public string? IsoCountryCode { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
