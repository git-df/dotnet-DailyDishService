using Domain.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly CacheOptions _cacheOptions;
        private readonly IFusionCache _cache;

        public CacheRepository(IFusionCache cache, IOptions<CacheOptions> cacheOptions)
        {
            _cache = cache;
            _cacheOptions = cacheOptions.Value;
        }

        public async Task<T> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
            => await _cache.GetOrDefaultAsync<T>(cacheKey, token: cancellationToken);

        public async Task SetAsync(string cacheKey, object value, CancellationToken cancellationToken = default)
            => await _cache.SetAsync(cacheKey, value, _cacheOptions.Options, token: cancellationToken);
    }
}
