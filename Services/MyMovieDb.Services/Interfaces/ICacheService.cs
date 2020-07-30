using System;
using System.Threading.Tasks;
using MyMovieDb.Services.Models;

namespace MyMovieDb.Services.Interfaces
{
    public interface ICacheService : ISingletonService
    {
        Task<T> GetOrCreate<T>(string key, CacheExpiration cacheExpiration, Func<Task<T>> getData);
    }
}