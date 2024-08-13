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

        /// <summary>
        /// Get value from cache
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="cacheKey">Cache value key</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Object T</returns>
        public async Task<T> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
            => await _cache.GetOrDefaultAsync<T>(cacheKey, token: cancellationToken);

        /// <summary>
        /// Save value in cache
        /// </summary>
        /// <param name="cacheKey">Cache value key</param>
        /// <param name="value">Value to save</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task SetAsync(string cacheKey, object value, CancellationToken cancellationToken = default)
            => await _cache.SetAsync(cacheKey, value, _cacheOptions.Options, token: cancellationToken);
    }
}
