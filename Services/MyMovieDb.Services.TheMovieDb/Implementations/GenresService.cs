using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyMovieDb.Services.Interfaces;
using MyMovieDb.Services.Models;
using MyMovieDb.Services.TheMovieDb.Constants;
using MyMovieDb.Services.TheMovieDb.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Genres;
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

        public async Task<GenreModel?> GetGenreById(int id, string language)
        {
            var genreResult = await GetGenres(language);
            if (genreResult == null || genreResult.Genres.Count == 0)
            {
                logger.LogWarning("No genres");
                return null;
            }

            var genre = genreResult.Genres.FirstOrDefault(t => t.Id == id);
            if (genre == null)
            {
                logger.LogWarning("Genre with {Id} doesn't exists", id);
                return null;
            }

            return genre;
        }

        public async Task<GenreListModel?> GetGenres(string language)
        {
            return await this.cacheService.GetOrCreate(CacheConstants.GetGenres, CacheExpiration.OneWeek, async () => await GetGenresFromApi(language));
        }

        private async Task<GenreListModel?> GetGenresFromApi(string language)
        {
            base.AddLanguageParameter(language);
            var genreResult =  await movieDbHttpService.Get<GenresListResult>(ApiUrlConstants.GenresMovieList, base.Parameters);
            if (!genreResult.IsSucess)
            {
                return null;
            }

            GenreListModel? genresModel = null;
            
            if (genreResult.Result != null && genreResult.Result.Genres != null)
            {
                var genres = genreResult.Result.Genres;
                genresModel = new GenreListModel();
                foreach (var genre in genres)
                {
                    if (genre.Id == null || genre.Name == null)
                    {
                        continue;
                    }

                    genresModel.Genres.Add(new GenreModel()
                    {
                        Id = genre.Id.Value,
                        Name = genre.Name
                    });
                }
            }

            return genresModel;
        }
    }
}