using System.Text.Json.Serialization;

namespace MyMovieDb.Services.TheMovieDb.Models.Http
{
    public class HttpServiceError
    {
        [JsonPropertyName("status_message")]
        public string StatusMessage { get; set; } = null!;


        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }
    }
}