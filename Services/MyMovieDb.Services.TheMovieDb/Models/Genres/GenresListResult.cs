using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using MyMovieDb.Services.TheMovieDb.Interfaces;

namespace MyMovieDb.Services.TheMovieDb.Models.Genres
{
    public class GenresListResult : IServiceResult
    {
        [JsonPropertyName("genres")]
        public List<GenreResult>? Genres { get; set; }
    }
}
