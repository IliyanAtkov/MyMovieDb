using MyMovieDb.Services.Interfaces;
using MyMovieDb.Services.TheMovieDb.Models.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMovieDb.Services.TheMovieDb.Interfaces
{
    public interface IMovieDbHttpService : IService
    {
        Task<HttpServiceResult<T>> Get<T>(string url, Dictionary<string, string>? parameters = null) where T : class, IServiceResult;
    }
}