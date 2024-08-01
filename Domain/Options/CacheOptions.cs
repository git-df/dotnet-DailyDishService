using Microsoft.Extensions.Caching.Memory;

namespace Domain.Options
{
    public class CacheOptions
    {
        public int SlidingExpiration { get; set; } = 100;
        public int AbsoluteExpiration { get; set; } = 1000;
        public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;

        public MemoryCacheEntryOptions Options => new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(SlidingExpiration))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(AbsoluteExpiration))
            .SetPriority(Priority);
    }
}
