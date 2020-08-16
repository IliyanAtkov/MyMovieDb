using MyMovieDb.Services.TheMovieDb.Models.Genres;
using System;
using System.Collections.Generic;

namespace MyMovieDb.Services.TheMovieDb.Models.Movies
{
    public class MoviesListModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? PosterPath { get; set; }

        public string? Overview { get; set; }

        public ICollection<GenreModel> Genres { get; set; } = new List<GenreModel>();

        public string? BackdropPath { get; set; }

        public double? VoteAverage { get; set; }
    }
}
