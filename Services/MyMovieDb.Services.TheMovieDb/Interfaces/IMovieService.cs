using MyMovieDb.Services.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMovieDb.Services.TheMovieDb.Interfaces
{
    public interface IMovieService : IService
    {
        Task<IEnumerable<MoviesListModel>> GetNowPlaying(string language, int page = 1);
        Task<IEnumerable<MoviesListModel>> GetPopular(string language, int page = 1);
        Task<IEnumerable<MoviesListModel>> GetUpcoming(string language, int page = 1);
    }
}