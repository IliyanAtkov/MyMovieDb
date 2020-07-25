using MyMovieDb.Services.TheMovieDb.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyMovieDb.Services.TheMovieDb.Models.Movies
{
    public class BaseMoviesResult : IServiceResult
    {
        [JsonPropertyName("page")]
        public int? Page { get; set; }

        [JsonPropertyName("results")]
        public List<BaseMovieListResult>? Results { get; set; }

        [JsonPropertyName("total_results")]
        public int? TotalResults { get; set; }

        [JsonPropertyName("total_pages")]
        public int? TotalPages { get; set; }
    }
}