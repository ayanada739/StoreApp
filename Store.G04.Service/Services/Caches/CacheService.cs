﻿using StackExchange.Redis;
using Store.G04.Core.Sevices.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Caches
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheKeyAsync(string key)
        {
            var cacheResponse =  await _database.StringGetAsync(key);
            if (cacheResponse.IsNullOrEmpty) return null;
            return cacheResponse.ToString();
        }

        public async Task SetCatcheKeyAsync(string key, object response, TimeSpan expireTime)
        {
            if (response is null) return;

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


            await _database.StringSetAsync(key, JsonSerializer.Serialize( response), expireTime);
        }
    }
}
