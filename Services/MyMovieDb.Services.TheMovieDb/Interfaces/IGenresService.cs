using System.Threading.Tasks;
using MyMovieDb.Services.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Genres;

namespace MyMovieDb.Services.TheMovieDb.Interfaces
{
    public interface IGenresService : IService
    {
        Task<GenresListResult?> GetGenres(string language);

        Task<GenresListResult?> GetGenreById(int id, string language);
    }
}