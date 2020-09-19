using MyMovieDb.Services.TheMovieDb.Constants;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Http;
using MyMovieDb.Services.TheMovieDb.Models.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieDb.Services.TheMovieDb.Implementations
{
    public class MovieService : BaseListService, IMovieService
    {
        private readonly TheMovieDbHttpService movieDbHttpService;
        private readonly IGenresService genresService;

        public MovieService(TheMovieDbHttpService movieDbHttpService, IGenresService genresService)
        {
            this.movieDbHttpService = movieDbHttpService;
            this.genresService = genresService;
        }

        public async Task<IEnumerable<MoviesListModel>> GetNowPlaying(string language, int page = 1)
        {
            base.AddPageAndLanguageParameters(language, page);
            var nowPlayingMoviesResult = await movieDbHttpService.Get<BaseMoviesResult>(ApiUrlConstants.MoviesNowPlaying, base.Parameters);
            return await GetMoviesListModel(nowPlayingMoviesResult, language);
        }

        private async Task<List<MoviesListModel>> GetMoviesListModel(HttpServiceResult<BaseMoviesResult> baseMoviesResult, string language)
        {
            var model = new List<MoviesListModel>();

            if (!baseMoviesResult.IsSucess)
            {
                return model;
            }

            List<BaseMoviesListResult> movies = new List<BaseMoviesListResult>();
            if (baseMoviesResult.Result != null && baseMoviesResult.Result.Results != null)
            {
                movies = baseMoviesResult.Result.Results;
            }


            foreach (var movie in movies)
            {
                if (movie.Id == null)
                {
                    continue;
                }

                var movieModel = new MoviesListModel()
                {
                    Title = movie.Title,
                    BackdropPath = movie.BackdropPath,
                    VoteAverage = movie.VoteAverage,
                    Id = movie.Id.Value,
                    Overview = movie.Overview,
                    PosterPath = movie.PosterPath
                };

                if (movie.ReleaseDate != null)
                {
                    if (DateTime.TryParse(movie.ReleaseDate, out DateTime releaseDate))
                    {
                        movieModel.ReleaseDate = releaseDate;
                    }
                }

                if (movie.GenreIds != null)
                {
                    foreach (var genreId in movie.GenreIds)
                    {
                        var genreResult = await genresService.GetGenreById(genreId, language);
                        if (genreResult == null)
                        {
                            continue;
                        }

                        movieModel.Genres.Add(new Models.Genres.GenreModel()
                        {
                            Name = genreResult.Name != null ? genreResult.Name : string.Empty,
                            Id = genreResult.Id
                        });
                    }
                }

                model.Add(movieModel);
            }

            return model;
        }
    }
}
