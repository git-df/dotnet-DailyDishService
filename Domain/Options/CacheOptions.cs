using Microsoft.Extensions.Caching.Memory;
using ZiggyCreatures.Caching.Fusion;

namespace Domain.Options
{
    public class CacheOptions
    {
        public int DefaultCacheDuration { get; set; }
        public CacheItemPriority DefaultPriority { get; set; }

        public FusionCacheEntryOptions Options => new FusionCacheEntryOptions()
            .SetDuration(TimeSpan.FromSeconds(DefaultCacheDuration))
            .SetPriority(DefaultPriority);
    }
}
