using ECommerce.Domain.Interfaces;
using ECommerce.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.Services.Servicies
{
    public class CacheService(ICacheRepository repository) : ICacheService
    {
        public async Task<string> GetAsync(string cacheKey)
        {
            var chachedData = await repository.GetAsync(cacheKey);
            return chachedData;
        }
         
        public async Task setAsync(string cacheKey, object CachedValue, TimeSpan TTL)
        {
            var value = JsonSerializer.Serialize(CachedValue);

            await repository.SetAsync(cacheKey, value, TTL);
        }
    }
}
