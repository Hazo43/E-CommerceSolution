using E_Commerce.Domain.Intreface;
using E_Commerce.Services_Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository cacheRepository;

        public CacheService(ICacheRepository _cacheRepository)
        {
            cacheRepository = _cacheRepository;
        }
        public async Task<string?> GetAsync(string Cachekey)
        {
            return await cacheRepository.GetAsync(Cachekey);
        }

        public async Task SetAsync(string Cachekey, object Cachevalue, TimeSpan TimeToLive)
        {
            var Value = JsonSerializer.Serialize(Cachevalue , new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            await cacheRepository.SetAsync(Cachekey, Value, TimeToLive);
        }
    }
}
