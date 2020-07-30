using System.Text.Json.Serialization;

namespace MyMovieDb.Services.TheMovieDb.Models.Genres
{
    public class GenreResult
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}