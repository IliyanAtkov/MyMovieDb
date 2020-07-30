using MyMovieDb.Services.TheMovieDb.Constants;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Movies;
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

        public async Task GetNowPlaying(string language, int page = 1)
        {
            base.AddPageAndLanguageParameters(language, page);
            var result = await movieDbHttpService.Get<BaseMoviesResult>(ApiUrlConstants.MoviesNowPlaying, base.Parameters);
            //var genres = await genresService.GetGenreById(language);

        }
    }
}
