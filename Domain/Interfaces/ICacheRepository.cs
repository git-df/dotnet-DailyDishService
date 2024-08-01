

namespace Domain.Interfaces
{
    public interface ICacheRepository
    {
        T Get<T>(string cacheKey);
        void Set(string cacheKey, object value);
    }
}
