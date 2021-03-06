﻿using MyMovieDb.Services.TheMovieDb.Models.Companies;
using MyMovieDb.Services.TheMovieDb.Models.Countries;
using MyMovieDb.Services.TheMovieDb.Models.Genres;
using MyMovieDb.Services.TheMovieDb.Models.Languages;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyMovieDb.Services.TheMovieDb.Models.Movies
{
    public class MovieResult
    {
        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string? BackdropPath { get; set; }

        [JsonPropertyName("budget")]
        public int Budget { get; set; }

        [JsonPropertyName("genres")]
        public List<GenreResult>? Genres { get; set; }

        [JsonPropertyName("homepage")]
        public string? Homepage { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("imdb_id")]
        public string? ImdbId { get; set; }

        [JsonPropertyName("original_language")]
        public string? OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")]
        public string? OriginalTitle { get; set; }

        [JsonPropertyName("overview")]
        public string? Overview { get; set; }

        [JsonPropertyName("popularity")]
        public decimal Popularity { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("production_companies")]
        public List<CompanyResult>? ProductionCompanies { get; set; }

        [JsonPropertyName("production_countries")]
        public List<CountryResult>? ProductionCountries { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        [JsonPropertyName("revenue")]
        public int Revenue { get; set; }

        [JsonPropertyName("runtime")]
        public int? Runtime { get; set; }

        [JsonPropertyName("spoken_languages")]
        public List<LanguageResult>? SpokenLanguages { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("tagline")]
        public string? Tagline { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("video")]
        public bool HasVideo { get; set; }

        [JsonPropertyName("vote_average")]
        public decimal VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }
    }
}
