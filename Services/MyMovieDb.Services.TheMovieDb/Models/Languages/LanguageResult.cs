using System.Text.Json.Serialization;

namespace MyMovieDb.Services.TheMovieDb.Models.Languages
{
    public class LanguageResult
    {
        [JsonPropertyName("iso_639_1")]
        public string? IsoLanguageCode { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}