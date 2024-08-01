using Domain.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheOptions _cacheOptions;

        public CacheRepository(IMemoryCache memoryCache, IOptions<CacheOptions> cacheOptions)
        {
            _memoryCache = memoryCache;
            _cacheOptions = cacheOptions.Value;
        }

        public T Get<T>(string cacheKey) 
            => _memoryCache.TryGetValue(cacheKey, out T value) ? value : default;

        public void Set(string cacheKey, object value)
            => _memoryCache.Set(cacheKey, value, _cacheOptions.Options);
    }
}
