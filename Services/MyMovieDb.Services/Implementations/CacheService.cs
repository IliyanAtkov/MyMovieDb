using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MyMovieDb.Services.Interfaces;
using MyMovieDb.Services.Models;

namespace MyMovieDb.Services.Implementations
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public CacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public virtual async Task<T> GetOrCreate<T>(string key, CacheExpiration cacheExpiration, Func<Task<T>> getData)
        {
            if (GetFromCache(key, out T data))
            {
                return data;
            }

            return await AddToCache(key, cacheExpiration, getData);
        }

        protected virtual bool GetFromCache<T>(string key, out T data)
        {
            if (memoryCache.TryGetValue(key, out data) && data != null)
            {
                return true;
            }

            return false;
        }

        protected virtual async Task<T> AddToCache<T>(string key, CacheExpiration cacheExpiration, Func<Task<T>> getData)
        {
            await semaphore.WaitAsync();
            try
            {
                T data;
                if (GetFromCache(key, out data))
                {
                    return data;
                }

                data = await getData();
                if (IsDataValid(data))
                {
                    memoryCache.Set(key, data, GetAbsoluteExpiration(cacheExpiration));
                }

                return data;
            }
            finally
            {
                semaphore.Release();
            }
        }

        protected virtual bool IsDataValid<T>(T data)
        {
            if (data != null)
            {
                return true;
            }

            return false;
        }

        private TimeSpan GetAbsoluteExpiration(CacheExpiration cacheExpiration)
        {
            TimeSpan timeSpan;

            switch (cacheExpiration)
            {
                case CacheExpiration.HalfAnHour:
                    timeSpan = TimeSpan.FromMinutes(30);
                    break;
                case CacheExpiration.OneHour:
                    timeSpan = TimeSpan.FromHours(1);
                    break;
                case CacheExpiration.FourHours:
                    timeSpan = TimeSpan.FromHours(4);
                    break;
                case CacheExpiration.OneDay:
                    timeSpan = TimeSpan.FromDays(1);
                    break;
                case CacheExpiration.OneWeek:
                    timeSpan = TimeSpan.FromDays(7);
                    break;
                default:
                    timeSpan = TimeSpan.Zero;
                    break;
            }

            return timeSpan;
        }
    }
}
