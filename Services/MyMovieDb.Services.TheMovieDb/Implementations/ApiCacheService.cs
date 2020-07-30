using Microsoft.Extensions.Caching.Memory;
using MyMovieDb.Services.Implementations;
using MyMovieDb.Services.TheMovieDb.Interfaces;

namespace MyMovieDb.Services.TheMovieDb.Implementations
{
    public class ApiCacheService : CacheService, IApiCacheService
    {
        public ApiCacheService(IMemoryCache memoryCache) : base(memoryCache)
        {
        }

        protected override bool IsDataValid<T>(T data)
        {
            if (!base.IsDataValid(data))
            {
                return false;
            }

            var isSucessResult = data as IIsSucessResult;
            if (isSucessResult != null)
            {
                return isSucessResult.IsSucess;
            }

            return false;
        }
    }
}