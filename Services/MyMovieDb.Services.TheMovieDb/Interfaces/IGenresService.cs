using System.Threading.Tasks;
using MyMovieDb.Services.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Genres;

namespace MyMovieDb.Services.TheMovieDb.Interfaces
{
    public interface IGenresService : IService
    {
        Task<GenreListModel?> GetGenres(string language);

        Task<GenreModel?> GetGenreById(int id, string language);
    }
}