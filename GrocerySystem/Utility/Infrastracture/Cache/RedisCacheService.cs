using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Cache;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Infrastracture.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _redisDb;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _redisDb.StringSetAsync(key, value, expiry ?? TimeSpan.FromMinutes(30));
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _redisDb.StringGetAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _redisDb.KeyDeleteAsync(key);
        }
    }

}
