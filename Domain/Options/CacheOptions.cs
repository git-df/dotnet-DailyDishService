using Microsoft.Extensions.Caching.Memory;

namespace Domain.Options
{
    public class CacheOptions
    {
        public int AbsoluteExpiration { get; set; }
        public CacheItemPriority Priority { get; set; }

        public MemoryCacheEntryOptions Options => new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(AbsoluteExpiration))
            .SetPriority(Priority);
    }
}
