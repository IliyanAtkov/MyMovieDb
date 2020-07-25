using MyMovieDb.Services.Constants;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Movies;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyMovieDb.Services.TheMovieDb.Implementations
{
    public class MovieService : BaseListService, IMovieService
    {
        private readonly TheMovieDbHttpService movieDbHttpService;

        public MovieService(TheMovieDbHttpService movieDbHttpService)
        {
            this.movieDbHttpService = movieDbHttpService;
        }

        public async Task GetNowPlaying(string language, int page = 1)
        {
            base.AddPageParameter(page);
            base.AddLanguageParameter(language);

            var result = await movieDbHttpService.Get<BaseMoviesResult>("movie/now_playing", base.Parameters);
        }
    }
}
