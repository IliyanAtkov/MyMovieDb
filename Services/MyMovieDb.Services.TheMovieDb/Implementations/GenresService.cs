using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyMovieDb.Services.Interfaces;
using MyMovieDb.Services.Models;
using MyMovieDb.Services.TheMovieDb.Constants;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Genres;
using MyMovieDb.Services.TheMovieDb.Models.Http;
using System.Linq;

namespace MyMovieDb.Services.TheMovieDb.Implementations
{
    public class GenresService : BaseListService, IGenresService
    {
        private readonly TheMovieDbHttpService movieDbHttpService;
        private readonly ICacheService cacheService;
        private readonly ILogger<GenresService> logger;

        public GenresService(TheMovieDbHttpService movieDbHttpService, ICacheService cacheService, ILogger<GenresService> logger)
        {
            this.movieDbHttpService = movieDbHttpService;
            this.cacheService = cacheService;
            this.logger = logger;
        }

        public async Task<GenresListResult?> GetGenreById(int id, string language)
        {
            var genreResult = await GetGenres(language);
            if (genreResult == null || genreResult.Genres == null)
            {
                logger.LogWarning("No genres");
                return null;
            }

            var genre = genreResult.Genres.FirstOrDefault(t => t.Id == id);
            if (genre == null)
            {
                logger.LogWarning("Genre with {Id} doesn't exists", id);
            }

            return genreResult;
        }

        public async Task<GenresListResult?> GetGenres(string language)
        {
            var genres = await this.cacheService.GetOrCreate(CacheConstants.GetGenres, CacheExpiration.OneWeek, async () => await GetGenresFromApi(language));
            return genres?.Result;
        }

        private async Task<HttpServiceResult<GenresListResult>> GetGenresFromApi(string language)
        {
            base.AddLanguageParameter(language);
            return await movieDbHttpService.Get<GenresListResult>(ApiUrlConstants.GenresMovieList, base.Parameters);
        }
    }
}