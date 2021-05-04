
using MyMovieDb.Services.TheMovieDb.Models.Companies;
using MyMovieDb.Services.TheMovieDb.Models.Countries;
using MyMovieDb.Services.TheMovieDb.Models.Genres;
using System.Collections.Generic;

namespace MyMovieDb.Services.TheMovieDb.Models.Movies
{
    public class MovieModel
    {
        public bool Adult { get; set; }

        public string? BackdropPath { get; set; }

        public int Budget { get; set; }

        public ICollection<GenreModel> Genres { get; set; } = new List<GenreModel>();

        public string? Homepage { get; set; }

        public int Id { get; set; }

        public string? ImdbId { get; set; }

        public string? OriginalLanguage { get; set; }

        public string? OriginalTitle { get; set; }

        public string? Overview { get; set; }

        public decimal Popularity { get; set; }

        public string? PosterPath { get; set; }

        public ICollection<CompanyModel> ProductionCompanies { get; set; } = new List<CompanyModel>();

        public ICollection<CountryModel> ProductionCountries { get; set; } = new List<CountryModel>();

        public string? ReleaseDate { get; set; }

        public int Revenue { get; set; }

        public int? Runtime { get; set; }

        public string? Status { get; set; }

        public string? Tagline { get; set; }

        public string? Title { get; set; }

        public bool HasVideo { get; set; }

        public decimal VoteAverage { get; set; }

        public int VoteCount { get; set; }
    }
}