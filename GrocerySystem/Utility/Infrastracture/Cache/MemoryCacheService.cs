using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastracture.Cache
{

    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            _memoryCache.Set(key, value, expiry ?? TimeSpan.FromMinutes(30));
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync(string key)
        {
            _memoryCache.TryGetValue(key, out string? value);
            return Task.FromResult(value);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }

}
